import axios from 'axios';

// take data from a given object
const selectData = ({ data }) => data;

// make api call
export default endPoint => axios.get(endPoint).then(selectData);
