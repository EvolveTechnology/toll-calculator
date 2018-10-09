import * as tollCalculator from "./tollCalculator";
import * as tollCheckout from './tollCheckout';
import * as database from './database';

// Poor mans database
let db = database;
tollCheckout.setDb(database);

function setDb(database) {
  db = database;
  tollCheckout.setDb(database);
}
// ------------

// Camera sends a passage
function main(passage) {
  if (tollCalculator.isTollFree(passage)) {
    let lineItem = tollCheckout.lineItemFromPassage(passage, 0);
    db.save('transactions', lineItem);
  } else {
    let lineItems = tollCheckout.checkout(passage);
    lineItems.forEach(lineItem => db.save('transactions', lineItem));
  }
}

export {
  main,
  setDb,
}
