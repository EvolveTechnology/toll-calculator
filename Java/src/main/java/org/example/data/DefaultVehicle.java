package org.example.data;

public class DefaultVehicle implements Vehicle{

    private final String type;

    public DefaultVehicle(String type){
        this.type = type;
    }
    @Override
    public String getType() {
        return this.type;
    }


}
