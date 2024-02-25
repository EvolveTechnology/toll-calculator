package org.example.data;

import java.time.LocalDateTime;
import java.util.List;

import lombok.Data;
@Data
public class TollFeeRequest {
    private String vehicle;
    private List<LocalDateTime> dates;

}
