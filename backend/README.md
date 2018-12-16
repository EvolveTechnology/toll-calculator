# Back End

## Tech Stack

Express App + webtask + Babel + ES6 modules + Axios

> JSDocs syntax everywhere!

### Network

This back end service uses a webtask running an express app. The express app exposes two `POST` endpoints.

The configuration for this webtask is in `backend/index.js`.

This back end service can be run in `DEV` mode, which relies on the apiMock component to be running. In production mode it consumes a `.secrets` file. Not included in the repository!

The secrets file points to the location of vehicle data and holidays API.

If the data processing in either endpoint fails, a `try-catch` block collects the `error` and returns an empty response.

To make the API calls, [`axios`](https://github.com/axios/axios) is used. Axios is easier to test and returns JavaScript objects, unlike `fetch`.

### Core

The core is found inside `backend/src/`. It follows a functional programming style, supported by modern ES6 and beyond.

The core uses ES6 modules to share functions across files. In order to join together the ES6 modules with the webtask, we must `babel` the source code. The following line in `backend/package.json` transpiles the ES6 modules to JavaScript readable by the webtask.

```
"build": "yarn coverage && babel src -d build",
```

Once transpiled, the source code is allocated in the `backend/build` folder, also not included in the repository, and then it is imported into `backend/index.js`.

When you run `yarn start-all` from the root of the project, the code in `backend/src/` will be transpiled, and the `build` folder will be created. You'll see a lot of logs indicating the creation of new files and a build folder.

This project also uses the plugin `transform-object-rest-spread`, which allows us to use object deconstruction to easily work with objects without mutating them.

To deploy the webtask we have to bundle the build folder, by using this command:

```
"wt-deploy": "wt create --bundle index.js --secrets-file .secrets"
```

### Testing

Testing has been done with [Jest.](https://jestjs.io/)

To test asynchronous calls to other APIs I've used [`axios-mock-adapter`.](https://github.com/ctimmerm/axios-mock-adapter).

From the very beginning testing was put in place to secure the behaviour given by the proposed problem.

However it was quickly noticed that the code in the proposal has bugs and had to be rewritten.
