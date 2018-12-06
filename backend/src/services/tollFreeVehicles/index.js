export const tollFree = ['Motorbike', 'Tractor', 'Emergency', 'Diplomat', 'Foreign', 'Military'];

/**
 * check if a vehicle is included in toll free list
 *
 * @param {type} - vehicle type
 * @return whether or not the vehicle is tollFree
 */
export default ({ type }) => tollFree.includes(type);
