package util;

/**
 * Static precondition methods.
 */
public final class Precondition {
    private Precondition() {
    }

    public static void isNotNull(Object o, String oName) {
        if (o == null) {
            throw new NullPointerException(oName);
        }
    }
}
