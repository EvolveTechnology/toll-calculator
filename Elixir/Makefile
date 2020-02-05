.PHONY: clean compile check-format test

DKR_IMAGE:=elixir:1.10.0-alpine

DKR_RUN:=docker run \
	--rm \
	--interactive \
	--tty \
	--volume $(shell pwd):/work \
	--workdir /work \
	$(DKR_IMAGE)

clean:
	$(DKR_RUN) mix clean

compile:
	$(DKR_RUN) mix compile --warnings-as-errors

check-format:
	$(DKR_RUN) mix format --check-formatted

test:
	$(DKR_RUN) mix test --trace

check: clean compile check-format test
