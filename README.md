# MyContoso

## Purpose

MyContoso is a contrived internal company app inspired by real enterprise systems.  
It is designed to demonstrate how client applications evolve under real delivery pressure, and how architectural decisions impact correctness, ownership, and maintainability over time.

The app intentionally includes:

*   Shared state
*   Isolated state
*   Navigation
*   Domain ownership
*   Cross-cutting concerns

## Core Concepts (Domain)

These are the **authoritative domain concepts**. Even if you stub them initially, these are the things that must eventually have a single source of truth.

### Employee

Represents a person employed by the company.

Key properties (illustrative, not exhaustive):

*   EmployeeId
*   Name
*   Role / Title
*   Department
*   ProfileSummary
*   AccreditationStatus (summary)

### CompanyUpdate

Represents a company-wide update or announcement.

Key properties:

*   UpdateId
*   Title
*   Body
*   PublishedDate
*   Author (Employee reference)

### Policy

Represents an internal company policy or handbook entry.

Key properties:

*   PolicyId
*   Title
*   Category
*   LastUpdated
*   Content (rich text or markdown)

### Accreditation

Represents a professional accreditation or compliance requirement.

Key properties:

*   AccreditationId
*   Name
*   Description
*   Status (e.g. Valid, Expired, Pending)
*   ExpiryDate

## App Sections / Features

You do **not** need to implement all of these fully. They exist to justify structure and navigation.

### 1. Company Feed

**Purpose:**  
Demonstrates shared, long-lived state.

**Description:**  
A feed of company-wide updates visible to all users.

**Characteristics:**

*   Same data regardless of navigation path
*   Updates infrequently
*   Expected to be cached
*   Natural fit for a singleton page + shared state service

**Typical interactions:**

*   View list of updates
*   Tap update to view details

### 2. Employee Directory

**Purpose:**  
Demonstrates isolated state and transient navigation.

**Description:**  
A list of employees with drill-down into individual profiles.

**Characteristics:**

*   List is shared
*   Profile pages are user-specific
*   Easy to demonstrate state leakage if mis-scoped

**Typical interactions:**

*   Browse employees
*   View employee profile
*   Navigate between different profiles

This is your **canonical example** for transient pages.

### 3. Employee Profile

**Purpose:**  
Demonstrates correctness bugs caused by lifetime mistakes.

**Description:**  
Detailed view of a single employee.

**Characteristics:**

*   Profile data must never bleed between employees
*   Navigation parameters matter
*   Disposal matters

This is where you can demonstrate:

*   Why singleton pages are dangerous here
*   Why ownership of state matters

### 4. Policies & Handbook

**Purpose:**  
Demonstrates static or semi-static content.

**Description:**  
A browsable list of company policies.

**Characteristics:**

*   Read-only
*   Rarely changes
*   Minimal state
*   Good example of content that does _not_ need complex architecture

Useful for showing restraint.

### 5. Accreditations (Optional slice)

**Purpose:**  
Demonstrates derived state and cross-cutting visibility.

**Description:**  
Shows accreditation status for employees.

**Characteristics:**

*   Data often summarised
*   Status appears in multiple places (profile, dashboard)
*   Raises questions about authoritative source of truth

Good for discussing:

*   Where derived state lives
*   Why duplication becomes dangerous

## Navigation Model

High-level routes (conceptual, not implementation):

*   `/feed`
*   `/employees`
*   `/employees/{id}`
*   `/policies`
*   `/policies/{id}`

You can deliberately start with:

*   Magic strings
*   Weak typing

Then later evolve to:

*   Centralised route definitions
*   Stronger guarantees

## Backend Assumptions (Minimal)

The backend is **not the focus**, but it must exist conceptually.

Assume:

*   REST-style endpoints
*   DTO-based contracts
*   No auth concerns for the demo

Example endpoints:

*   `GET /company-updates`
*   `GET /employees`
*   `GET /employees/{id}`
*   `GET /policies`

Hard-coded or in-memory responses are fine.
