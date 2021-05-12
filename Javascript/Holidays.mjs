export const OffsetDate = (sourceDate, offset) => {
    let date = new Date(sourceDate);
    date.setDate(sourceDate.getDate() + offset);
    return date;
}

export const EasterDate = y => {
    // Source: https://jsfiddle.net/9v2af1w5/ 
    const c = Math.floor(y/100);
    const n = y - 19*Math.floor(y/19);
    const k = Math.floor((c - 17)/25);
    let i = c - Math.floor(c/4) - Math.floor((c - k)/3) + 19*n + 15;
    i = i - 30*Math.floor((i/30));
    i = i - Math.floor(i/28)*(1 - Math.floor(i/28)*Math.floor(29/(i + 1))*Math.floor((21 - n)/11));
    let j = y + Math.floor(y/4) + i + 2 - c + Math.floor(c/4);
    j = j - 7*Math.floor(j/7);
    const l = i - j;
    const m = 3 + Math.floor((l + 40)/44);
    const d = l + 28 - 31*Math.floor(m/4);

    return new Date(y, m-1, d);
}

export const MidsummerDayDate = year => {
    let date = new Date(year, 5, 20);
    let weekday = date.getDay();
    // Move the date to closest Saturday forward
    return OffsetDate(date, 6 - weekday);
}

export const AllSaintsDayDate = year => {
    let date = new Date(year, 9, 31);
    let weekday = date.getDay();
    // Move the date to closest Saturday forward
    return OffsetDate(date, 6 - weekday);
}

export const GetNonWeekendHolidays = year => {
    const easter = EasterDate(year);
    const midsummerDay = MidsummerDayDate(year);
    const allSaintsDay = AllSaintsDayDate(year);


    // Not listing holidays that are guaranteed Saturday or Sunday
    return [
        new Date(year, 0, 1),
        new Date(year, 0, 6),
        OffsetDate(easter, -2), // Friday before Easter
        easter,
        OffsetDate(easter, 1), // Monday after Easter
        new Date(year, 4, 1),
        OffsetDate(easter, 39), // Ascension Day
        OffsetDate(easter, 49), // Pentecost Eve
        OffsetDate(easter, 50), // Pentecost Day
        new Date(year, 5, 6),
        OffsetDate(midsummerDay, -1), // Midsummer Eve
        midsummerDay,
        allSaintsDay,
        new Date(year, 11, 24),
        new Date(year, 11, 25),
        new Date(year, 11, 26),
        new Date(year, 11, 31),
    ]
}