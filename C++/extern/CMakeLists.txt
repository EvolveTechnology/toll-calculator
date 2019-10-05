set(USE_SYSTEM_TZ_DB ON CACHE BOOL "Use system TZ database")
set(ENABLE_DATE_TESTING OFF CACHE BOOL "Enable date unit tests")

add_subdirectory(date)
add_subdirectory(range-v3)

# date library uses std::uncaught_exception(), which is deprecated, by default.
# By adding the following define, we can tell date and its dependencies to use
# the new std::uncaught_exceptions() instead.
target_compile_definitions(date INTERFACE HAS_UNCAUGHT_EXCEPTIONS)