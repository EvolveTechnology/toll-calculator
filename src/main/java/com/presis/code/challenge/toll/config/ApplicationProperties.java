package com.presis.code.challenge.toll.config;

import java.io.IOException;
import java.io.InputStream;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.fasterxml.jackson.dataformat.javaprop.JavaPropsMapper;
import com.presis.code.challenge.toll.model.TollMaster;

public class ApplicationProperties {

	public ApplicationProperties() {}
	
	public TollMaster readMaster() {
        
        try (InputStream input = getClass().getClassLoader().getResourceAsStream("application.properties"))
        {
            JavaPropsMapper mapper = new JavaPropsMapper();
            return mapper.readValue(input, TollMaster.class);
        } catch (IOException ioex) {
            Logger.getLogger(getClass().getName()).log(Level.ALL, "IOException Occured while loading properties file::::" +ioex.getMessage());
        }
        return null;
    }
}
