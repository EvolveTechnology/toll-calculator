add_subdirectory("${PROJECT_SOURCE_DIR}/extern/googletest" "extern/googletest")

mark_as_advanced(
    BUILD_GMOCK BUILD_GTEST BUILD_SHARED_LIBS
    gmock_build_tests gtest_build_samples gtest_build_tests
    gtest_disable_pthreads gtest_force_shared_crt gtest_hide_internal_symbols
)

set_target_properties(gtest PROPERTIES FOLDER extern)
set_target_properties(gtest_main PROPERTIES FOLDER extern)
set_target_properties(gmock PROPERTIES FOLDER extern)
set_target_properties(gmock_main PROPERTIES FOLDER extern)

macro(package_add_test TESTNAME)
    add_executable(${TESTNAME} ${ARGN})
    target_link_libraries(${TESTNAME} TollFeeCalculator gtest gtest_main)
    add_test(NAME ${TESTNAME} COMMAND ${TESTNAME})
    set_target_properties(${TESTNAME} PROPERTIES FOLDER tests)
endmacro()

package_add_test(easter_date_test easter_date.cpp)
package_add_test(get_daily_fee_test get_daily_fee.cpp)
package_add_test(get_hourly_fee_test get_hourly_fee.cpp)
package_add_test(get_time_of_day_fee_test get_time_of_day_fee.cpp)
package_add_test(is_toll_free_date_test is_toll_free_date.cpp)
package_add_test(toll_fee_calculator_test toll_fee_calculator.cpp)
