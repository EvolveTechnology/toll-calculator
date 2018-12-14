import axios from "axios";
import MockAdapter from "axios-mock-adapter";
import { queryOne, queryAll, endpoint } from "..";
import { mockData, expected, expectedAll } from "./mock";

// This sets the mock adapter on the default instance
const mock = new MockAdapter(axios);

mock.onPost(`${endpoint}/vehicle`).replyOnce(config => {
  if (config.data === '"QNX-473"') {
    return [
      200,
      {
        ...mockData
      }
    ];
  }
  return [401, { id: null }];
});

mock.onPost(`${endpoint}/all`).replyOnce(200, [{ ...mockData }]);

describe("query for one", () => {
  const fallback = {};
  const callback = jest.fn();
  const regNum = "QNX-473";
  const badRegNum = "abc-123";

  it("fetches one vehicle", async () => {
    await queryOne(regNum, callback, fallback);
    expect(callback).toHaveBeenCalledWith(expected);
  });

  it("gracefully handles failure to fetch", async () => {
    await queryOne(badRegNum, callback, fallback);
    expect(callback).toHaveBeenCalledWith(fallback);
  });
});

describe("query for all", () => {
  it("fetches one vehicle", async () => {
    const callback = jest.fn();
    const fallback = {};
    await queryAll(callback, fallback);
    expect(callback).toHaveBeenCalledWith(expectedAll);
  });

  it("gracefully handles failure", async () => {
    const callback = jest.fn();
    const fallback = {};

    await queryAll(callback, fallback);
    expect(callback).toHaveBeenCalledWith(fallback);
  });
});
