import { render, screen } from "@testing-library/react";
import ReceiptContainer from "./ReceiptContainer";

test("render container for receipts", () => {
  const vehicle = {
    reg: "ABC123",
    type: "car",
    passages: {
      "2020-12-24": ["15:00:29", "15:35:19"],
    },
  };
  render(<ReceiptContainer vehicleData={vehicle} />);
  const container = screen.getByTestId("receipt-container");
  expect(container).toBeInTheDocument();
});
