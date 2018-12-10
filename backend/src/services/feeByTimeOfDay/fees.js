import { LOW, MEDIUM, HIGH } from '../../constants';

const asMinutes = (h, m = 0) => h * 60 + m;
const makeRange = (...from) => (...to) => [from, to].map(hour => asMinutes(...hour));

// We should have a way to allow admins to set ranges and prices
// unlike in intervalMarker/ which is an implementation to create
// unique intervals
export default [
  { range: makeRange(6)(6, 29), fee: LOW },
  { range: makeRange(6, 30)(6, 59), fee: MEDIUM },
  { range: makeRange(7)(7, 59), fee: HIGH },
  { range: makeRange(8)(8, 29), fee: MEDIUM },
  { range: makeRange(8, 30)(14, 59), fee: LOW },
  { range: makeRange(15)(15, 29), fee: MEDIUM },
  { range: makeRange(15, 30)(16, 29), fee: HIGH },
  { range: makeRange(17)(17, 59), fee: MEDIUM },
  { range: makeRange(18)(18, 29), fee: LOW },
];
