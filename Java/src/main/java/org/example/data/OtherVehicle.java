package org.example.data;

public class OtherVehicle implements Vehicle{

    private final String type;

    public OtherVehicle(String type){
        this.type = type;
    }
    @Override
    public String getType() {
        return this.type;
    }


}
