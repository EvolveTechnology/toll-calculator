const tollFreeDates = {
    2013: {
        January: {
            1: true,
        },
        March: {
            28: true,
            29: true,
        },
        April: {
            1: true,
            30: true,
        },
        May: {
            1: true,
            8: true,
            9: true,
        },
        June: {
            1: true,
            8: true,
            9: true,
        },
        July: {
            ...Array.from(Array(31))
                .map((e, i) => i + 1)
                .map((x) => ({ [x]: true })),
        },
        November: {
            1: true,
        },
        December: {
            24: true,
            25: true,
            26: true,
            31: true,
        },
    },

};


module.exports = Object.freeze(tollFreeDates);