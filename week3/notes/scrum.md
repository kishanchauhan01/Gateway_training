## 1. What is Scrum?

- Scrum is an `Agile` framework that helps a team build software in small, organized cycles instead of trying to build everything at once.

For example, imagine your team has to build a HelpDesk application:

- `Sprint 1`: Employee login and ticket creation.
- `Sprint 2`: Ticket listing and status updates.
- `Sprint 3`: Comments and notifications.

- Each sprint has a goal, selected tasks, and a fixed time period.

- A sprint is commonly one or two weeks, though teams can choose a different duration.

## 2. Product backlog

- A Product Backlog is an ordered list of everything the team might need to build, improve, or fix in the product.

- Think of it as the application's master to-do list.

Example HelpDesk product backlog:

| Backlog item           | Priority |
| ---------------------- | -------- |
| Employee login         | High     |
| Create support ticket  | High     |
| View my tickets        | High     |
| Assign ticket to agent | High     |
| Update ticket status   | Medium   |
| Email notifications    | Low      |
| Dark mode              | Low      |

- The backlog is not necessarily fixed. New requirements can be added, and priorities can change.

- `Important`: The Product Owner is accountable for managing the Product Backlog and its ordering. Developers help clarify and estimate the work.

## 3. Story points

- Story points are estimates of the relative effort, complexity, and uncertainty involved in completing a task.

- `They are not hours or days.`

For example:

| Task                             | Story points |
| -------------------------------- | ------------ |
| Change button text               | 1            |
| Add login validation             | 2            |
| Create ticket API                | 3            |
| Implement authentication flow    | 5            |
| Build a complex reporting module | 8            |

Teams often use a Fibonacci-like scale:

```text
1 2 3 5 8 13
```

- Why not estimate everything in hours?

- `Because` a task's difficulty isn't just about coding time. It may involve unclear requirements, dependencies, testing, and technical risks.

For example:

- `Task A`: 2 points — straightforward API endpoint.
- `Task B`: 5 points — API endpoint with complex authorization and edge cases.
  The team estimates together. Story points are not a measure of an individual developer's productivity.

## 4. Sprint planning

- Sprint planning is the meeting where the Scrum Team decides:

1. Why? What is the goal of this sprint?
2. What? Which backlog items can we take into this sprint?
3. How? How will we complete the selected work?

Example:

- Your HelpDesk team plans a one-week sprint.

**Sprint 1 — Employee can report an issue**

| Selected work      | 11 story points |
| ------------------ | --------------- |
| Employee login     | 3 points        |
| Create ticket form | 3 points        |
| Create ticket API  | 5 points        |

`Sprint Goal`: Employees can log in and submit a support ticket.

- The team selects work based on its priority, capacity, and understanding of the tasks. It should not blindly commit to more work than it can `reasonably complete`.

## 5. Daily standup

- A Daily Scrum is a short, daily event for Developers to inspect progress toward the Sprint Goal and adjust their plan.

- A common format is answering three questions:
    1. What did I complete yesterday?
    2. What am I working on today?
    3. Is anything blocking my progress?

Example:
```text
Yesterday, I completed the login API. Today, I'll work on ticket creation. I'm blocked because I need clarification about the ticket category field.
```

- A few important points:
    - Usually timeboxed to 15 minutes.
    - It is not a lengthy status-reporting meeting.
    - The purpose is to coordinate work and identify blockers.




