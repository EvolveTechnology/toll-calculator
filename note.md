Program optimization
1. Use constant classes to avoid strings being written dead in programs.
2. Give MSG prompt for parameter or logical exception
3. Use Lambda expressions and functional programming to simplify code.
4. Start Junit5 to support TDD
5, use the YAML configuration file, configure the time period corresponding to the amount of charges, rather than writing in the program.
Logic optimization
1. This program only supports requests within one day.
2. Make non-null check for vehicle and time set parameters respectively.
Ps: Currently I create the Service object manually (in the constructor) in the test class or calling class, or @Autowired if it's a Spring project
