import { render, screen } from "@testing-library/react";
import Receipt from "./Receipt";

test("render a receipt", () => {
  render(<Receipt />);
  const receipt = screen.getByTestId("receipt");
  expect(receipt).toBeInTheDocument();
});
