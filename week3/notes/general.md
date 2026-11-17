## What is HelpDesk?

- A HelpDesk is a software application used to manage and resolve support requests (tickets).

Imagine you work at a company with 200 employees.

- An employee faces a problem:
    - Their laptop isn't working.
    - They cannot log in.
    - They need access to a particular application.
    - Their internet connection is down.

How do they report these problems?

- Instead of messaging someone individually or sending random emails, they use a HelpDesk application.

- The application allows them to create a support ticket, and the support team can manage and resolve it.

- `Yes`, HelpDesk is software. More specifically, it's a `ticket` management and support management system.

- What does a ticket contain?

For example:

```text
Ticket #1025
In Progress

Title: Unable to access company email
Description: I cannot log in to my company email account.
Created by: Rahul
Assigned to: Support Agent A
Priority: High
Category: Account Access
Created: 17 November 2026
```

A HelpDesk application might also have features such as:
- User registration and login.
- Creating and updating tickets.
- Assigning tickets to agents.
- Changing ticket status.
- Adding comments.
- Managing priorities and categories.
- Tracking ticket history.

## What are User Stories?

- A user story is a short description of a feature written from the perspective of the person who will use it.

It explains:
- Who wants something?
- What do they want?
- Why do they want it?

A common format is:

```text
As a [type of user],
I want [some functionality],
So that [I get some benefit].
```

Example: Creating a ticket
Suppose we're building the HelpDesk application.

- A user story could be:

```text
As an employee, I want to create a support ticket so that I can report my technical problems to the support team.
```


| Part |	Meaning |
| ---  |   ---
| As an employee |	Who?
| I want to create a support ticket |	What?
| So that I can report my technical problems |	Why?

## What are Acceptance Criteria?

- Acceptance criteria are specific conditions that a feature must satisfy to be considered complete and acceptable.

- A user story tells us what the user wants.
- `Acceptance criteria` tell us exactly what the feature must do.

Let's continue with our ticket creation example.

`User story:`

```text
As an employee, I want to create a support ticket so that I can report my technical problems to the support team.
```

`Acceptance criteria`
1. An authenticated employee can create a ticket.
2. The ticket must have a title and description.
3. The title cannot be empty.
4. The employee must select a category.
5. The system should automatically assign a unique ticket ID.
6. The newly created ticket should have an initial status of Open.
7. The employee should receive confirmation after successfully creating the ticket.

- These conditions make the requirement much clearer.

**Why do we need acceptance criteria?**

- Imagine you're a developer and your manager simply says:

- `"Build a ticket creation feature."`

- You might build a form that accepts only a title.

- But the manager expects a title, description, category, unique ID and default status.

- Without clear acceptance criteria, different people can interpret the same feature differently.

Acceptance criteria establish a shared understanding between:
- Product owners
- Developers
- QA engineers
- Other stakeholders

**Acceptance criteria in Given–When–Then format**

- Might encounter this format, especially in Agile projects.

```text
Given – When – Then

GIVEN
The employee is logged into the HelpDesk application.
The initial condition or context.


WHEN
The employee submits a ticket with a valid title, description and category.
The action being performed.


THEN
The system creates the ticket, assigns a unique ID and displays a success message.
The expected outcome.
```

- This format is particularly useful because it makes requirements testable.

- For example, a QA engineer can verify whether the expected outcome actually happens.