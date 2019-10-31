/*****************************
Author:       Duane Billue
Date:         2019-10-31
Description:  Schedule a Job
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPTINT ON;

-- Create Program
DBMS_SCHEDULER.CREATE_PROGRAM
(
	program_name => 'Prog_Name',
	program_type => 'Stored Prcedure',
	program_action => 'Emp_Report'
);

-- Add Parameters
DBMS_SCHEDULER.DEFINE_PROGRAM_ARGUMENT
(
	program_name => 'Prog_Name',
	argument_name => 'Dept_ID',
	argument_position => 1, 
	argument_type => 'NUMBER',
	default_value => '50'
);

-- Create Job w/ Number of Arguments
DBMS_SCHEDULER.CREATE_JOB
(
	'JOB_NAME',
	program_name => 'Prog_Name',
	start_date => SYSTIMESTAMP,
	repeat_interval => 'FREQ_DAILY',
	number_of_arguments => 1,
	enabled => TRUE
);
