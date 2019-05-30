import com.mashape.unirest.http.Unirest;
import com.mashape.unirest.http.exceptions.UnirestException;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

class ApiHelper {
  private static final Logger logger = LogManager.getLogger(ApiHelper.class);

  private static final String URI = "https://calendarific.com/api/v2/holidays";
  private static final String API_KEY = "b0072287a620b11d67611ac26428530d7cc4583d";

  /**
   * Call CALENDARIFIC Api to check if given date is holiday or not. The response JSON looks like:
   * {'meta': 'HTTP_CODE', 'response': {'holidays': [list of events related to specific holiday]} }
   * if the 'holidays' list is empty, it means the specified date is not a holiday, for more
   * information check: {@see https://calendarific.com/api-documentation}
   *
   * @param year given year
   * @param month given month
   * @param day given day
   * @return true if given date is holiday, false otherwise
   */
  static boolean isHoliday(int year, int month, int day) {
    //    TODO The API service do not support SWEDEN specific holidays such as national day June 6th

    try {
      var result =
          Unirest.get(URI)
              .header("accept", "application/json")
              .queryString("api_Key", API_KEY)
              .queryString("country", "SE")
              .queryString("year", year)
              .queryString("month", month)
              .queryString("day", day)
              .asJson()
              .getBody()
              .getObject()
              .getJSONObject("response")
              .getJSONArray("holidays");
      logger.debug("API result = " + result.toString());
      return result.length() != 0;
    } catch (UnirestException e) {
      logger.error(e.getMessage());
      return false;
    }
  }
}
