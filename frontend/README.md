# Front End

## Tech Stack

React + Create-React-App + React Router Dom + Axios

## Features

This application has two main features.

1. [`Home`](https://nice-sky.surge.sh/)

   In the home path, one can query for a vehicle and see detailed daily information.

   Each day is represented by a circular progress bar. A full bar means 60 SEK. Empty indicates 0 SEK.

   If the day was a weekend or holiday, an icon is shown.

   Otherwise the total calculation of fees for the day is shown. From 0 to 60 SEK.

   The Home page is found in `frontend/src/containers/Home`.

2) [`Dashboard`](https://nice-sky.surge.sh/dashboardd)

   In the dashboard path, one can query for all vehicles in the database and see a summary for each vehicle.

   The loading of vehicles is done by clicking the bottom left button. Once loaded the user can use the same button to refresh.

   The user can also filter vehicles by type, or sort them from highest to lowest fee.

   The user can also click on each element to further access detailed data for the vehicle.

   The user can also search for registration numbers.

   To exit this detailed mode, the user can click the bottom left button, now named `Show All`.

   The Dashboard page is found in `frontend/src/containers/Dashboard`

In addition, when the user scrolls down, a `Top` button is shon at the bottom right, which performs a soft scroll to top.

If a search query returns no vehicles, an empty box is shown. If there are network errors, a printer failing to is shown.

If the user is offline, an offline Icon is shown.

This done using the Offline container, which wraps the whole application. Found in `frontend/src/containers/Offline`.

## Routes

Following the features there are three routes:

1. `/` for Home.
2. `/dashboard` for Dashboard.
3. For undefined paths we have the 404 page.

## State Management

There six pieces of state in this application.

1.  `AnimatedProgress`

    Controls the progress of the stroke in the svg `Circle`.

2.  `Fabs`

    Controls whether or not to show the `Top` button.

3.  `Spinner`

    Controls whether or not to mount the `Spinner`.

4.  `Offline`

    Controls whether or not to load the `Routes`, or the offline `Placeholder`.

5.  `Dashboard`

    Controls the user interactions in the dashboard page. That is, queries, filters, sorting and individual selection.

    It also stores the vehicles fetched from the network.

    Finally, it also controls the state of the loading `Spinner` when fetching vehicles, and whether or not to show the network error `Placeholder`.

6.  `Home`

    Controls the user interactions in the home page. That is, query, sorting, and data for the vehicle fetched from the network.

The React function `setState` function is used to modify the state. Where callbacks are needed, functions that use `setState` is passed as part of a Component method. Where possible `functions` are bound to the Component to extract away abstraction and complexity.

## Side Effects

Side Effects means in this context user input and network requests.

Inputs are handled using `React.createRef()`.

Network requests use axios and callbacks to `setState`. When a request fails, setState is called with a fallback state.

A Spinner is shown when a network request begins. It unmounts `1.5 seconds` after the request is completed. For more detail see the `componentDidUpdate` method of `frontend/src/components/Spinner/index.js`.

## Utils & Helpers

A utility function in this project, is a function which can easily be shared by components and containers, even open sourced to other projects.

Helpers are local and concern only a simple abstraction to help a component or container.

For example the Fabs component has a helper method, which is a function bound to the React Component. This function simply calculates whether or not to show the `Top` button. It has no use anywhere else.

In the `frontend/src/utils` we have more generic functions, which accumulate values, partially apply arguments to functions or verify if a callback is a function before calling it.

## Testing

Testing has been done with [Jest](https://jestjs.io/) and [Enzyme](https://airbnb.io/enzyme/).

To test asynchronous calls to the backend service I've used [`axios-mock-adapter`.](https://github.com/ctimmerm/axios-mock-adapter).

For this part of the project I first sketched and worked on implementing some basic layout, by writing CSS and React Components.

Once the layout was in place I began to implement features and back them up with tests.

When needed browser methods have been mocked.
