package com.evolve_technology.calculator.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.PropertySource;
import org.springframework.context.annotation.Scope;
import org.springframework.context.support.PropertySourcesPlaceholderConfigurer;
import org.springframework.test.context.ContextConfiguration;

import com.evolve_technology.calculator.properties.TollConfiguration;
import com.evolve_technology.calculator.service.TollFeeService;
import com.evolve_technology.calculator.service.TollFreeDatesService;
import com.evolve_technology.calculator.service.TollFreeVehicleService;
import com.evolve_technology.calculator.service.impl.TollFeeServiceImpl;
import com.evolve_technology.calculator.service.impl.TollFreeDatesServiceImpl;
import com.evolve_technology.calculator.service.impl.TollFreeVehiclesServiceImpl;
import com.evolve_technology.calculator.util.TollRules;
import com.evolve_technology.calculator.util.TollUtil;

@ContextConfiguration
@PropertySource("classpath:application.properties")
@ComponentScan(basePackageClasses = TollConfiguration.class)
public class AppConfig {
	 // this bean will be injected into the OrderServiceTest class
    @Bean
    @Scope("prototype")
    public TollFeeService tollFeeService() {
    	TollFeeService tollFeeService = new TollFeeServiceImpl();
        // set properties, etc.
        return tollFeeService;
    }
    
    @Bean
    @Scope("prototype")
    public TollFreeDatesService tollFreeDatesService() {
    	TollFreeDatesService tollFreeDatesService = new TollFreeDatesServiceImpl();
        // set properties, etc.
        return tollFreeDatesService;
    }
    
    @Bean
    @Scope("prototype")
    public TollFreeVehicleService tollFreeVehicleService() {
    	TollFreeVehicleService tollFreeVehicleService = new TollFreeVehiclesServiceImpl();
        // set properties, etc.
        return tollFreeVehicleService;
    }
    
    
    @Bean
    @Scope("prototype")
    public TollRules tollRules() {
    	TollRules tollRules = new TollRules();
        // set properties, etc.
        return tollRules;
    }
    
    @Bean
    @Scope("prototype")
    public TollUtil tollUtil() {
    	TollUtil tollUtil = new TollUtil();
        // set properties, etc.
        return tollUtil;
    }
    
//    @Bean()
//    public TollConfiguration tollConfiguration() {
//    	TollConfiguration tollConfiguration = new TollConfiguration();
////    	tollConfiguration.setDates(null);
////    	tollConfiguration.setMonths(null);
////    	tollConfiguration.setVehicles(null);
////    	tollConfiguration.setYear(null);
//        // set properties, etc.
//        return tollConfiguration;
//    }
    
    @Bean
    public static PropertySourcesPlaceholderConfigurer propertyConfigInDev() {
        return new PropertySourcesPlaceholderConfigurer();
    }
}
