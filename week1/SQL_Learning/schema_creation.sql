CREATE TABLE IF NOT EXISTS departments (
    department_id INT PRIMARY KEY,
    department_name VARCHAR(50) NOT NULL,
    floor_number INT NOT NULL,
    annual_budget DECIMAL(12, 2) NOT NULL,
    location_code VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS employees (
    employee_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    department_id INT REFERENCES departments (department_id),
    job_title VARCHAR(50) NOT NULL,
    hire_date DATE NOT NULL,
    salary DECIMAL(10, 2) NOT NULL,
    manager_id INT REFERENCES employees (employee_id)
);

CREATE TABLE IF NOT EXISTS patients (
    patient_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    birth_date DATE NOT NULL,
    gender VARCHAR(10),
    city VARCHAR(50),
    state VARCHAR(2),
    insurance_provider VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS admissions (
    admission_id INT PRIMARY KEY,
    patient_id INT NOT NULL REFERENCES patients (patient_id),
    attending_doctor_id INT REFERENCES employees (employee_id),
    admission_date DATE NOT NULL,
    discharge_date DATE,
    primary_diagnosis VARCHAR(100) NOT NULL,
    room_type VARCHAR(20) NOT NULL,
    total_billing DECIMAL(10, 2) NOT NULL,
    status VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS treatments (
    treatment_id INT PRIMARY KEY,
    admission_id INT NOT NULL REFERENCES admissions (admission_id),
    doctor_id INT REFERENCES employees (employee_id),
    treatment_name VARCHAR(100) NOT NULL,
    treatment_date TIMESTAMP NOT NULL,
    cost DECIMAL(10, 2) NOT NULL,
    outcome_code VARCHAR(20)
);

CREATE INDEX IF NOT EXISTS idx_employees_department_id ON employees (department_id);

CREATE INDEX IF NOT EXISTS idx_employees_manager_id ON employees (manager_id);

CREATE INDEX IF NOT EXISTS idx_employees_job_title ON employees (job_title);

CREATE INDEX IF NOT EXISTS idx_patients_state ON patients (state);

CREATE INDEX IF NOT EXISTS idx_patients_city ON patients (city);

CREATE INDEX IF NOT EXISTS idx_patients_insurance_provider ON patients (insurance_provider);

CREATE INDEX IF NOT EXISTS idx_admissions_patient_id ON admissions (patient_id);

CREATE INDEX IF NOT EXISTS idx_admissions_doctor_id ON admissions (attending_doctor_id);

CREATE INDEX IF NOT EXISTS idx_admissions_date ON admissions (admission_date);

CREATE INDEX IF NOT EXISTS idx_admissions_diagnosis ON admissions (primary_diagnosis);

CREATE INDEX IF NOT EXISTS idx_admissions_room_status ON admissions (room_type, status);

CREATE INDEX IF NOT EXISTS idx_treatments_admission_id ON treatments (admission_id);

CREATE INDEX IF NOT EXISTS idx_treatments_doctor_id ON treatments (doctor_id);

CREATE INDEX IF NOT EXISTS idx_treatments_date ON treatments (treatment_date);

CREATE INDEX IF NOT EXISTS idx_treatments_outcome ON treatments (outcome_code);