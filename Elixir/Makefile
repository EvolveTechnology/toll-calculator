.PHONY: clean deps compile check-format credo test

DKR_IMAGE:=elixir-dev:local

DKR_RUN:=docker run \
	--rm \
	--interactive \
	--tty \
	--volume $(shell pwd):/work \
	--workdir /work \
	$(DKR_IMAGE)

build-image:
	docker build -t $(DKR_IMAGE) -f Dockerfile .

clean:
	$(DKR_RUN) mix clean

deps:
	$(DKR_RUN) mix deps.get

compile:
	$(DKR_RUN) mix compile --warnings-as-errors

check-format:
	$(DKR_RUN) mix format --check-formatted

test:
	$(DKR_RUN) mix test --trace

credo:
	$(DKR_RUN) mix credo --strict

check: clean deps compile check-format credo test
