export function activateButton() {
  const { show } = this.state;
  const fromTop = window.scrollY;

  if (fromTop > 100) {
    return !show && this.setState({ show: true });
  }
  return show && this.setState({ show: false });
}
