import React, { Fragment } from "react";
import AnimatedProgress from "../AnimatedProgress";
import Type from "../Type";
import Weekend from "../Weekend";
import Holiday from "../Holiday";
import TollFreeVehicle from "../TollFreeVehicle";
import typeIconSelector from "../Type/helpers";
import { isHoliday, isWeekend } from "../../utils";

import "./results.css";

const Results = ({ sorted, isTollFree, type, regNum, allTimeTotalFee }) => (
  <Fragment>
    {type && (
      <div className="results">
        <Type
          src={typeIconSelector(type)}
          type={type}
          totalFee={allTimeTotalFee}
          regNum={regNum}
        />
      </div>
    )}
    {isTollFree ? (
      <TollFreeVehicle />
    ) : (
      <div className="results-group">
        {sorted.map(({ day, totalFee, ...fee }) => {
          const holiday = isHoliday(fee);
          const weekend = isWeekend(fee);
          const freeDay = holiday || weekend;

          return (
            <AnimatedProgress
              key={day}
              label={day}
              value={totalFee}
              freeDay={freeDay}
            >
              {holiday && <Holiday key={day} label={day} />}
              {weekend &&
                !holiday && <Weekend key={day} label={day} {...fee} />}
            </AnimatedProgress>
          );
        })}
      </div>
    )}
  </Fragment>
);

export default Results;
