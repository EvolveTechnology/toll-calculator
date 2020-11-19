import { render, screen } from "@testing-library/react";
import ReceiptRow from "./ReceiptRow";

test("render receipt row", () => {
  render(<ReceiptRow />);
  const row = screen.getByTestId("row");
  expect(row).toBeInTheDocument();
});
