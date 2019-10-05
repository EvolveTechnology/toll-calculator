cmake_minimum_required(VERSION 3.13.4)

project(TollFeeCalculator VERSION 1.0 LANGUAGES CXX)

add_subdirectory(extern)

add_library(TollFeeCalculator INTERFACE)
target_include_directories(TollFeeCalculator INTERFACE
    $<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}>/include)
	
set(HEADERS
    include/toll_fee_calculator/date_time.h
    include/toll_fee_calculator/toll_fee_calculator.h
    include/toll_fee_calculator/vehicle.h
    include/toll_fee_calculator/detail/get_daily_fee.h
    include/toll_fee_calculator/detail/get_hourly_fee.h
    include/toll_fee_calculator/detail/get_time_of_day_fee.h
    include/toll_fee_calculator/detail/is_toll_free_date.h
    include/toll_fee_calculator/detail/time_of_day_range.h
)

target_link_libraries(TollFeeCalculator
    INTERFACE
        date
        range-v3
)
    
add_executable(TollFeeCalculatorIDE
    ${HEADERS}
    src/date_time.cpp
    src/main.cpp
    src/toll_fee_calculator.cpp
    src/vehicle.cpp
    src/detail/get_daily_fee.cpp
    src/detail/get_hourly_fee.cpp
    src/detail/get_time_of_day_fee.cpp
    src/detail/is_toll_free_date.cpp
    src/detail/time_of_day_range.cpp
)

target_link_libraries(TollFeeCalculatorIDE PRIVATE TollFeeCalculator)

option(PACKAGE_TESTS "Build the tests" ON)
if(PACKAGE_TESTS)
    enable_testing()
    add_subdirectory(tests)
endif()
