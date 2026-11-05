SELECT * FROM patients;

SELECT p.first_name, p.last_name, p.city FROM patients AS p;

SELECT DISTINCT e.job_title FROM employees AS e;

SELECT d.department_name
FROM departments AS d
WHERE d.annual_budget > 500000;

SELECT concat(p.first_name, p.last_name) AS patient_name
FROM patients AS p
WHERE p.state = 'CA'

SELECT a.patient_id
FROM admissions AS a
WHERE a.primary_diagnosis = 'Hypertension'

SELECT concat(e.first_name, e.last_name) AS employee_name
FROM employees AS e
WHERE e.salary >= 80000

SELECT a.*
FROM admissions AS a
WHERE a.total_billing BETWEEN 1000 AND 5000;

SELECT p.*
FROM patients AS p
WHERE p.birth_date > '1990-01-01';

SELECT t.treatment_name
FROM treatments AS t
WHERE t.cost > 250;

SELECT p.first_name, p.last_name
FROM patients AS p
ORDER BY p.last_name, p.first_name;

SELECT concat(e.first_name, e.last_name) AS employee_name
FROM employees AS e
ORDER BY e.hire_date DESC;

SELECT a.*
FROM admissions AS a
ORDER BY a.total_billing DESC;

SELECT t.*
FROM treatments AS t
ORDER BY t.cost DESC LIMIT 10;

SELECT p.*
FROM patients AS p
LIMIT 5 OFFSET 10;

SELECT COUNT(*) AS admission_count FROM admissions;

SELECT AVG(e.salary) FROM employees AS e;

SELECT MAX(a.total_billing) FROM admissions AS a;

SELECT MIN(t.cost) FROM treatments AS t;

SELECT SUM(d.annual_budget) FROM departments AS d;

SELECT p.*
FROM patients AS p
WHERE p.insurance_provider IS null;

SELECT p.*
FROM patients AS p
WHERE p.insurance_provider IS NOT null;

SELECT e.* FROM employees AS e WHERE e.manager_id IS NULL;

SELECT t.*
FROM treatments AS t
WHERE t.treatment_date >= TIMESTAMP '2024-01-01 00:00:00'

SELECT p.first_name
FROM patients AS p
WHERE p.first_name LIKE 'J%';

SELECT a.primary_diagnosis
FROM admissions AS a
WHERE a.primary_diagnosis LIKE '%Cardiac%'

SELECT e.last_name
FROM employees AS e
WHERE e.last_name LIKE '%son';

SELECT COUNT(DISTINCT p.insurance_provider) AS distinct_insurance_provider
FROM patients AS p;

SELECT a.admission_id, a.patient_id, a.total_billing*1.05 AS adjusted_billing FROM admissions AS a;

SELECT p.*
FROM patients AS p
WHERE p.city IN ('Chicago', 'Houston');

SELECT p.state, COUNT(*)
FROM patients AS p
GROUP BY p.state ORDER BY p.state;

SELECT e.job_title, AVG(e.salary) AS avg_salary
FROM employees AS e
GROUP BY e.job_title;

SELECT a.primary_diagnosis, SUM(a.total_billing) AS total_billing_per_diagnosis
FROM admissions AS a
GROUP BY a.primary_diagnosis
ORDER BY total_billing_per_diagnosis DESC;

SELECT a.room_type, AVG(a.total_billing) AS avg_total_billing_per_room
FROM admissions AS a
GROUP BY a.room_type;

SELECT e.department_id, COUNT(e.employee_id) AS employee_count
FROM employees AS e
GROUP BY e.department_id
HAVING COUNT(e.employee_id) > 5;

SELECT a.primary_diagnosis, AVG(a.total_billing) AS avg_total_billing_per_dignosis
FROM admissions AS a
GROUP BY a.primary_diagnosis
HAVING AVG(a.total_billing) > 1000
ORDER BY avg_total_billing_per_dignosis DESC;

SELECT p.state, COUNT(p.patient_id) AS patient_count_per_state
FROM patients AS p
GROUP BY p.state
HAVING COUNT(p.patient_id) > 20;

SELECT t.outcome_code, SUM(t.cost) AS total_cost_per_outcome
FROM treatments AS t
GROUP BY t.outcome_code;

SELECT e.job_title, MAX(e.salary) As max_salary
FROM employees AS e
GROUP BY e.job_title
HAVING MAX(e.salary) > 12000;

SELECT a.status, COUNT(a.status)
FROM admissions AS a
WHERE a.status <> 'Discharged'
GROUP BY a.status;

SELECT
    p.insurance_provider,
    SUM(a.total_billing) AS total_billing,
    AVG(a.total_billing) As avg_billing
FROM patients AS p
JOIN admissions AS a USING (patient_id)
GROUP BY p.insurance_provider
HAVING AVG(a.total_billing) > 8000;

SELECT p.city, p.state, COUNT(p.*) AS patient_count
FROM patients AS p
GROUP BY p.city, p.state
HAVING COUNT(p.*) > 3;

SELECT e.job_title, AVG(e.salary) AS avg_salary
FROM employees AS e
GROUP BY e.job_title
HAVING AVG(e.salary) BETWEEN 60000 AND 200000;

SELECT
    e.department_id,
    MAX(e.salary) AS max_salary_per_department,
    MIN(e.salary) AS min_salary_per_department
FROM employees AS e
GROUP BY e.department_id;