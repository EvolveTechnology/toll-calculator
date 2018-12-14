export function deltaSteps() {
  const { value } = this.props;
  const { percentage } = this.state;
  const delta = percentage + 1;

  this.percentage(delta);

  if (delta >= (value / 60) * 100) {
    clearInterval(this.interval);
  }
  return null;
}
