'use strict';

import * as tollCalculator from './tollCalculator';
import * as database from './database';

const MS_PER_HOUR = 60000;
const STAR_MODE_DURATION = 60;

let db = database;
function setDb(database) {
  db = database;
}

/**
 * StarMode makes the entity passing through a toll invulnerable to a fee.
 *
 * @param passage
 * @returns {Array} | A list of all previous passages within an hour of this one.
 */
function setStarMode(passage) {
  let previousPassage = {},
    starModeTimer = STAR_MODE_DURATION,
    starModePassagesList = [],
    minutesSinceLastPassage,
    passages = db.transactionsById(passage.id);

  for (let i = passages.length - 1; i >= 0; i--) {
    previousPassage = passages[i];

    minutesSinceLastPassage = (passage.date - previousPassage.date) / MS_PER_HOUR;
    if (minutesSinceLastPassage < STAR_MODE_DURATION) {
      // Easier to reason about StarMode counting down, instead of a diff in
      // minutes.
      starModeTimer = STAR_MODE_DURATION - minutesSinceLastPassage;
      starModePassagesList.push(previousPassage);
    } else {
      break;
    }
  }

  passage.starMode = starModeTimer;

  return starModePassagesList;
}

/**
 * @param passage
 * @param fee
 * @returns {{id: *, vehicleType: number, date: (Date|string), fee: *}}
 */
function lineItemFromPassage(passage, fee) {
  return {
    id: passage.id,
    vehicleType: passage.vehicleType,
    date: passage.date,
    fee: fee,
    starMode: passage.starMode,
  };
}

/**
 * @param max
 * @param current
 * @returns {number}
 */
let findMax = (max, current) => Math.max( max, current );

/**
 * All passages are treated as line items on a receipt. And at the end of the day
 * we 'print' it. This means that we also need to figure out which previous
 * transaction to credit if this one has a discount (Multiple passages; aka StarMode).
 * Also, this way we won't rewrite history.
 *
 * @param passage
 * @param passagesList
 * @returns {Array}
 */
function createLineItems(passage, passagesList) {
  let lineItems = [],
    fee = tollCalculator.getTollFromDate(passage.date);

  if (passagesList.length === 0) {
    lineItems.push(lineItemFromPassage(passage, fee));
    return lineItems;
  }

  let max = passagesList.map( el => el.fee )
    .reduce(findMax, -Infinity);

  if (max >= fee) {
    lineItems.push(lineItemFromPassage(passage, fee));
    lineItems.push(lineItemFromPassage(passage, -fee));
  } else {
    lineItems.push(lineItemFromPassage(passage, fee));
    lineItems.push(lineItemFromPassage(passage, -max))
  }

  return lineItems;
}

/**
 * Completes a transaction
 *
 * @param passage
 * @returns {Array} | lineItems
 */
function checkout(passage) {
  let starModePassagesList = setStarMode(passage);
  return createLineItems(passage, starModePassagesList);
}

export {
  setDb,
  lineItemFromPassage,
  setStarMode,
  createLineItems,
  checkout,
};
