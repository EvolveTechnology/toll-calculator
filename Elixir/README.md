# Elixir Toll Calculator

## Prerequisites

* [Make][make] - for managing project tasks
* [Docker][docker] - for running the project code without the need to setup Elixir/Erlang

## Getting Started

In order to run the code, start by building a Docker image capable of running Elixir:

```sh
$ make build-image
```

To fetch the needed libraries, compile the code, run static code analysis, and run unit tests:

```sh
$ make check
```

## Description

Since Elixir generates a lot of files when starting a new project, below is a list of the most relevant files to look at when grading the assignment:

* `lib/toll.ex` - the program entry point
* `lib/toll/*.ex` - the program code
* `test/**/*_test.ex` - unit tests

## Assumptions

* All passages occur in the same time zone.
* Since passages are free between 18:00 and 06:00, the code does not take any effects of daylight savings time changes into account. This might cause issues should the pricing model change.

[make]:https://www.gnu.org/software/make/manual/make.html
[docker]:https://docker.com
