import React from "react";
import "./search.css";

export function Search({ track, search, children }) {
  return (
    <div className="search-wrap">
      <div className="styled-form">
        <input
          ref={track}
          onChange={search}
          maxLength="7"
          placeholder="ABC-123"
        />
      </div>
    </div>
  );
}

export default Search;
