// Update: Added random Help functions
public class Helpers {
    
  /** Check if string is null or empty */
  public static boolean stringIsNullOrEmpty(String value){
    return isNull(value) || value.equals("");
  }

  /** Check if any datatype´s value is null */
  public static <T> boolean isNull(T obj){
      return obj == null;
  }
}