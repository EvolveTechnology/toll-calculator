import { render, screen } from "@testing-library/react";
import Receipt from "./Receipt";

test("render a receipt", () => {
  const vehicle = {
    reg: "ABC123",
    type: "car",
    passages: {
      "2020-12-24": ["15:00:29", "15:35:19"],
    },
  };
  render(<Receipt date="2020-12-24" vehicle={vehicle} />);
  const receipt = screen.getByTestId("receipt");
  expect(receipt).toBeInTheDocument();
});
