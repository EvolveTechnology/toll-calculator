function getTimeFromISOString(date) {
  let timeParts = date.toISOString().split('T');
  return timeParts[1].slice(0,5);
}

export default getTimeFromISOString;
