package test_utils;

public final class NameAndValue<T> {
    public final String name;
    public final T value;

    public NameAndValue(String name, T value)
    {
        this.name = name;
        this.value = value;
    }
}
