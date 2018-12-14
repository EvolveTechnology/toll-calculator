if (process.env.NODE_ENV === "production") {
  module.exports = {
    endpoint: process.env.REACT_APP_WT_ENDPOINT
  };
} else {
  module.exports = {
    endpoint: "http://localhost:1337"
  };
}
