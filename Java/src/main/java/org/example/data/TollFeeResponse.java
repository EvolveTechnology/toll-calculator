package org.example.data;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class TollFeeResponse {
    int  totalFees;
}
