import { render, screen } from "@testing-library/react";
import ReceiptContainer from "./ReceiptContainer";

test("render container for receipts", () => {
  render(<ReceiptContainer />);
  const container = screen.getByTestId("receipt-container");
  expect(container).toBeInTheDocument();
});
