import { render, screen } from "@testing-library/react";
import App from "./App";

test("renders tollfee calculator header", () => {
  render(<App />);
  const header = screen.getByText(/TollFee Calculator/i);
  expect(header).toBeInTheDocument();
});
