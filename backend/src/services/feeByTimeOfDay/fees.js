const asMinutes = (h = 0, m = 0) => h * 60 + m;
const minutesRange = (...from) => (...to) => [from, to].map(hour => asMinutes(...hour));

export default [
  { range: minutesRange(6)(6, 29), fee: 8 },
  { range: minutesRange(6, 30)(6, 59), fee: 13 },
  { range: minutesRange(7)(7, 59), fee: 18 },
  { range: minutesRange(8)(8, 29), fee: 13 },
  { range: minutesRange(8, 30)(14, 59), fee: 8 },
  { range: minutesRange(15)(15, 29), fee: 13 },
  { range: minutesRange(15, 30)(16, 29), fee: 18 },
  { range: minutesRange(17)(17, 59), fee: 13 },
  { range: minutesRange(18)(18, 29), fee: 8 },
];
