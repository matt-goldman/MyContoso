using MyContoso.App.Models;

namespace MyContoso.App.Services;

public class MockApiClient : IApiClient
{
    private static string GetInitials(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "?";

        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return parts.Length switch
        {
            0 => "?",
            1 => parts[0][..Math.Min(2, parts[0].Length)].ToUpperInvariant(),
            _ => $"{char.ToUpperInvariant(parts[0][0])}{char.ToUpperInvariant(parts[^1][0])}"
        };
    }

    private static readonly Random Random = new();

    private static readonly List<Employee> Employees =
    [
        new Employee(
            1,
            "Margaret Chen",
            GetInitials("Margaret Chen"),
            "Chief Executive Officer",
            "Executive",
            "Visionary leader with 20+ years of experience in technology and enterprise software. Previously served as COO at a Fortune 500 company. Known for transforming company culture and driving sustainable growth. Board member of several industry associations and advocate for women in technology.",
            "margaret.chen@contoso.com",
            "+61 2 9456 3100",
            "Level 15, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1972, 4, 18),
            [
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(12))
            ],
            "https://i.pravatar.cc/300?u=margaret.chen"),
        new Employee(
            2,
            "Rajesh Patel",
            GetInitials("Rajesh Patel"),
            "VP of Engineering",
            "Engineering",
            "Engineering executive with deep expertise in distributed systems and platform architecture. Built and scaled engineering teams from 20 to 200+ across multiple continents. PhD in Computer Science from Stanford. Passionate about engineering excellence and developer experience.",
            "rajesh.patel@contoso.com",
            "+61 2 9456 3101",
            "Level 15, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1978, 9, 5),
            [
                new Accreditation(9, "Kubernetes Administrator (CKA)", "Certified Kubernetes cluster administrator", "Valid", "External", DateTime.Now.AddMonths(18)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(10))
            ],
            "https://i.pravatar.cc/300?u=rajesh.patel"),
        new Employee(
            3,
            "Lisa Thompson",
            GetInitials("Lisa Thompson"),
            "Head of People & Culture",
            "People & Culture",
            "HR executive focused on building world-class employee experiences. Expert in organizational design, talent strategy, and culture transformation. Led successful integrations of 3 acquisitions. Certified executive coach and frequent speaker on future of work topics.",
            "lisa.thompson@contoso.com",
            "+61 2 9456 3102",
            "Level 15, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1980, 1, 22),
            [
                new Accreditation(18, "SHRM Senior Certified Professional", "Advanced HR certification", "Valid", "External", DateTime.Now.AddMonths(20)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(12))
            ],
            "https://i.pravatar.cc/300?u=lisa.thompson"),
        new Employee(
            4,
            "Sarah Mitchell",
            GetInitials("Sarah Mitchell"),
            "Chief Customer Officer",
            "Customer Success",
            "Customer-obsessed executive with 15 years building and scaling customer success organizations. Pioneered Contoso's customer health scoring methodology. Former management consultant specializing in customer experience transformation.",
            "sarah.mitchell@contoso.com",
            "+61 2 9456 3103",
            "Level 15, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1981, 7, 30),
            [
                new Accreditation(17, "Customer Success Certified", "Gainsight customer success certification", "Valid", "External", DateTime.Now.AddMonths(14)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(12))
            ],
            "https://i.pravatar.cc/300?u=sarah.mitchell"),
        new Employee(
            5,
            "Jennifer Walsh",
            GetInitials("Jennifer Walsh"),
            "Chief Compliance Officer",
            "Legal & Compliance",
            "Legal and compliance executive ensuring Contoso meets all regulatory requirements across jurisdictions. Former federal prosecutor with expertise in data privacy and corporate governance. Established Contoso's ethics program and whistleblower protections.",
            "jennifer.walsh@contoso.com",
            "+61 2 9456 3104",
            "Level 15, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1976, 11, 14),
            [
                new Accreditation(19, "Certified Compliance & Ethics Professional", "CCEP certification", "Valid", "External", DateTime.Now.AddMonths(22)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(12))
            ],
            "https://i.pravatar.cc/300?u=jennifer.walsh"),
        new Employee(
            6,
            "John Nakamura",
            GetInitials("John Nakamura"),
            "Senior Software Engineer",
            "Engineering",
            "Full-stack developer with 8 years of experience specializing in .NET and cloud-native architectures. Led the migration of legacy systems to microservices, reducing deployment time by 70%. Passionate about mentoring junior developers and contributing to open-source projects.",
            "john.nakamura@contoso.com",
            "+61 2 9456 3201",
            "Level 12, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1991, 3, 15),
            [
                new Accreditation(1, "AWS Solutions Architect", "Certified to design distributed systems on AWS", "Valid", "External", DateTime.Now.AddMonths(14)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(8))
            ],
            "https://i.pravatar.cc/300?u=john.nakamura"),
        new Employee(
            7,
            "Priya Sharma",
            GetInitials("Priya Sharma"),
            "Product Manager",
            "Product",
            "Strategic product leader with a background in data science and 6 years in B2B SaaS. Expert in translating complex customer needs into actionable roadmaps. Previously scaled a fintech product from $1M to $15M ARR. Known for cross-functional collaboration and customer-centric thinking.",
            "priya.sharma@contoso.com",
            "+61 2 9456 3202",
            "Level 12, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1989, 7, 22),
            [
                new Accreditation(7, "Certified Scrum Product Owner", "Qualified to lead agile product teams", "Valid", "External", DateTime.Now.AddMonths(10)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(6))
            ],
            "https://i.pravatar.cc/300?u=priya.sharma"),
        new Employee(
            8,
            "Marcus O'Brien",
            GetInitials("Marcus O'Brien"),
            "Lead UX Designer",
            "Design",
            "Award-winning designer with expertise in user research, interaction design, and design systems. Former agency creative director with clients including major Australian banks. Advocates for accessibility-first design and has spoken at multiple design conferences.",
            "marcus.obrien@contoso.com",
            "+61 2 9456 3203",
            "Level 8, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1985, 11, 8),
            [
                new Accreditation(8, "Certified Accessibility Specialist", "Qualified to audit WCAG compliance", "Valid", "External", DateTime.Now.AddMonths(18)),
                new Accreditation(1, "Health and Safety Certificate", "Site access certification", "Valid", "Internal", DateTime.Now.AddMonths(4))
            ],
            "https://i.pravatar.cc/300?u=marcus.obrien"),
        new Employee(
            9,
            "Elena Kowalski",
            GetInitials("Elena Kowalski"),
            "DevOps Team Lead",
            "Engineering",
            "Infrastructure architect specializing in Kubernetes, Terraform, and CI/CD pipelines. Built Contoso's current multi-region deployment infrastructure supporting 99.99% uptime. Previously worked at a Big Four consulting firm on enterprise cloud migrations.",
            "elena.kowalski@contoso.com",
            "+61 2 9456 3204",
            "Remote - Melbourne VIC",
            new DateTime(1988, 4, 30),
            [
                new Accreditation(9, "Kubernetes Administrator (CKA)", "Certified Kubernetes cluster administrator", "Valid", "External", DateTime.Now.AddMonths(20)),
                new Accreditation(10, "Azure Solutions Architect Expert", "Advanced Azure architecture certification", "Valid", "External", DateTime.Now.AddMonths(11)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(6))
            ],
            "https://i.pravatar.cc/300?u=elena.kowalski"),
        new Employee(
            10,
            "David Okonkwo",
            GetInitials("David Okonkwo"),
            "Senior Account Manager",
            "Sales",
            "Relationship-focused account manager with 10 years in enterprise software sales. Manages Contoso's top 15 accounts representing $8M in annual revenue. Expert negotiator known for turning at-risk accounts into long-term partnerships. Former professional rugby player.",
            "david.okonkwo@contoso.com",
            "+61 2 9456 3205",
            "Level 10, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1984, 9, 3),
            [
                new Accreditation(11, "Salesforce Certified Administrator", "Qualified to administer Salesforce CRM", "Valid", "External", DateTime.Now.AddMonths(7)),
                new Accreditation(1, "Health and Safety Certificate", "Site access certification", "Overdue", "Internal", DateTime.Now.AddDays(-15))
            ],
            "https://i.pravatar.cc/300?u=david.okonkwo"),
        new Employee(
            11,
            "Sophie Chen",
            GetInitials("Sophie Chen"),
            "Marketing Director",
            "Marketing",
            "Data-driven marketing executive with expertise in demand generation, brand strategy, and marketing automation. Grew Contoso's inbound leads by 250% over two years through integrated campaigns. MBA from Melbourne Business School with a background in psychology.",
            "sophie.chen@contoso.com",
            "+61 2 9456 3206",
            "Level 11, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1986, 12, 17),
            [
                new Accreditation(12, "Google Analytics Certified", "Advanced digital analytics certification", "Valid", "External", DateTime.Now.AddMonths(9)),
                new Accreditation(13, "HubSpot Marketing Software", "Certified in HubSpot marketing automation", "Valid", "External", DateTime.Now.AddMonths(15)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(6))
            ],
            "https://i.pravatar.cc/300?u=sophie.chen"),
        new Employee(
            12,
            "James Thornton",
            GetInitials("James Thornton"),
            "Finance Manager",
            "Finance",
            "CPA with 12 years of experience in corporate finance and FP&A. Oversees budgeting, forecasting, and financial reporting for APAC operations. Implemented new forecasting models that improved budget accuracy by 40%. Known for translating complex financials into actionable insights.",
            "james.thornton@contoso.com",
            "+61 2 9456 3207",
            "Level 14, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1982, 5, 28),
            [
                new Accreditation(2, "Accounting Level 1", "Finance department access", "Valid", "Internal", DateTime.Now.AddMonths(3)),
                new Accreditation(14, "CPA Australia", "Certified Practising Accountant", "Valid", "External", DateTime.Now.AddMonths(24)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Pending", "Internal", null)
            ],
            "https://i.pravatar.cc/300?u=james.thornton"),
        new Employee(
            13,
            "Aisha Rahman",
            GetInitials("Aisha Rahman"),
            "Security Engineer",
            "Engineering",
            "Cybersecurity specialist with expertise in threat detection, penetration testing, and security architecture. Led Contoso's SOC 2 Type II certification effort. Regular contributor to bug bounty programs and security research communities.",
            "aisha.rahman@contoso.com",
            "+61 2 9456 3208",
            "Level 12, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1993, 8, 11),
            [
                new Accreditation(15, "CISSP", "Certified Information Systems Security Professional", "Valid", "External", DateTime.Now.AddMonths(30)),
                new Accreditation(16, "Offensive Security Certified Professional", "Certified penetration tester", "Valid", "External", DateTime.Now.AddMonths(36)),
                new Accreditation(1, "Health and Safety Certificate", "Site access certification", "Valid", "Internal", DateTime.Now.AddMonths(10))
            ],
            "https://i.pravatar.cc/300?u=aisha.rahman"),
        new Employee(
            14,
            "Thomas Andersson",
            GetInitials("Thomas Andersson"),
            "Customer Success Manager",
            "Customer Success",
            "Dedicated CSM managing a portfolio of 45 mid-market accounts. Achieved 98% retention rate and 125% net revenue retention through proactive engagement. Former technical support lead who brings deep product knowledge to customer conversations.",
            "thomas.andersson@contoso.com",
            "+61 7 3456 7801",
            "Level 5, 480 Queen St, Brisbane QLD 4000",
            new DateTime(1990, 2, 14),
            [
                new Accreditation(17, "Customer Success Certified", "Gainsight customer success certification", "Valid", "External", DateTime.Now.AddMonths(8)),
                new Accreditation(1, "Health and Safety Certificate", "Site access certification", "Valid", "Internal", DateTime.Now.AddMonths(5))
            ],
            "https://i.pravatar.cc/300?u=thomas.andersson"),
        new Employee(
            15,
            "Michelle Torres",
            GetInitials("Michelle Torres"),
            "HR Business Partner",
            "People & Culture",
            "Strategic HR professional partnering with Engineering and Product teams. Expertise in talent acquisition, performance management, and organizational development. Champion of diversity and inclusion initiatives that increased underrepresented hiring by 35%.",
            "michelle.torres@contoso.com",
            "+61 2 9456 3210",
            "Level 11, 100 Barangaroo Ave, Sydney NSW 2000",
            new DateTime(1987, 6, 25),
            [
                new Accreditation(18, "SHRM Senior Certified Professional", "Advanced HR certification", "Valid", "External", DateTime.Now.AddMonths(16)),
                new Accreditation(3, "People Leadership", "Certified to hold management positions", "Valid", "Internal", DateTime.Now.AddMonths(6))
            ],
            "https://i.pravatar.cc/300?u=michelle.torres")
    ];

    private static Employee GetEmployee(string name) => Employees.First(e => e.Name == name);

    private static readonly List<CompanyUpdate> Updates =
    [
        new CompanyUpdate(
            1,
            "Welcome to Q1 2026!",
            "Happy New Year, Contoso family! As we kick off another exciting year, I want to take a moment to reflect on our incredible achievements in 2025 and share our vision for the months ahead. We grew our customer base by 40%, launched three new product lines, and expanded into two new markets. None of this would have been possible without each and every one of you. Let's make 2026 even better!",
            DateTime.Now.AddDays(-12),
            GetEmployee("Margaret Chen").Name,
            GetEmployee("Margaret Chen").Initials,
            GetEmployee("Margaret Chen").Role,
            GetEmployee("Margaret Chen").AvatarUrl,
            Random.Next(50, 120),
            Random.Next(15, 45),
            false),
        new CompanyUpdate(
            2,
            "Engineering All-Hands: Microservices Migration Update",
            "Great news from the Platform team! We've successfully completed Phase 2 of our microservices migration, achieving a 35% improvement in API response times and 99.97% uptime last quarter. Special shoutout to the DevOps crew who worked tirelessly over the holiday break to ensure zero customer impact. Phase 3 planning kicks off next week—watch your inboxes for meeting invites.",
            DateTime.Now.AddDays(-8),
            GetEmployee("Rajesh Patel").Name,
            GetEmployee("Rajesh Patel").Initials,
            GetEmployee("Rajesh Patel").Role,
            GetEmployee("Rajesh Patel").AvatarUrl,
            Random.Next(30, 80),
            Random.Next(20, 60),
            true),
        new CompanyUpdate(
            3,
            "New Sydney Office Grand Opening - You're Invited!",
            "Mark your calendars! Our brand new Sydney headquarters at Barangaroo will officially open on February 28th. The new space features collaborative work zones, a rooftop garden, state-of-the-art AV facilities, and yes—a barista bar serving complimentary coffee. All Sydney-based employees will receive their new access cards next week. RSVP for the opening celebration by February 14th.",
            DateTime.Now.AddDays(-5),
            GetEmployee("Lisa Thompson").Name,
            GetEmployee("Lisa Thompson").Initials,
            GetEmployee("Lisa Thompson").Role,
            GetEmployee("Lisa Thompson").AvatarUrl,
            Random.Next(80, 150),
            Random.Next(30, 70),
            false),
        new CompanyUpdate(
            4,
            "Customer Success Story: Meridian Healthcare",
            "Thrilled to share that our partnership with Meridian Healthcare has resulted in a 60% reduction in their patient scheduling errors and $2.3M in operational savings! This case study will be featured in next month's industry report. Congratulations to the entire Customer Success team, especially account lead David Okonkwo, for making this happen.",
            DateTime.Now.AddDays(-3),
            GetEmployee("Sarah Mitchell").Name,
            GetEmployee("Sarah Mitchell").Initials,
            GetEmployee("Sarah Mitchell").Role,
            GetEmployee("Sarah Mitchell").AvatarUrl,
            Random.Next(40, 90),
            Random.Next(10, 35),
            true),
        new CompanyUpdate(
            5,
            "Reminder: Annual Compliance Training Due Feb 28",
            "Just a friendly reminder that all employees must complete their annual compliance training modules by February 28th. This includes Data Privacy Essentials, Workplace Safety, and Anti-Harassment training. You can access all modules through the Learning Hub. Managers: please ensure your teams are on track—completion rates are currently at 67% company-wide.",
            DateTime.Now.AddDays(-1),
            GetEmployee("Jennifer Walsh").Name,
            GetEmployee("Jennifer Walsh").Initials,
            GetEmployee("Jennifer Walsh").Role,
            GetEmployee("Jennifer Walsh").AvatarUrl,
            Random.Next(15, 40),
            Random.Next(5, 20),
            false),
        new CompanyUpdate(
            6,
            "Introducing Our New Employee Wellness Program",
            "Your wellbeing matters to us! Starting March 1st, all full-time employees will have access to our enhanced wellness benefits including: free gym membership at partner locations, mental health support through Headspace, quarterly wellness stipends of $150, and flexible 'recharge' days. Check the HR portal for full details and enrollment.",
            DateTime.Now.AddHours(-6),
            GetEmployee("Lisa Thompson").Name,
            GetEmployee("Lisa Thompson").Initials,
            GetEmployee("Lisa Thompson").Role,
            GetEmployee("Lisa Thompson").AvatarUrl,
            Random.Next(100, 200),
            Random.Next(25, 55),
            false)
    ];

    private static readonly List<Policy> Policies =
    [
        new Policy(
            1,
            "Code of Conduct",
            "Ethics",
            DateTime.Now.AddMonths(-6),
            "Jennifer Walsh",
            "The latest code of conduct is now available for all staff. There are mandatory changes that all employees need to become familiar with.",
            """
            # Contoso Code of Conduct

            ## Purpose
            This Code of Conduct establishes the ethical standards and professional behaviour expected of all Contoso employees, contractors, and representatives.

            ## Core Principles

            ### 1. Integrity
            - Act honestly and transparently in all business dealings
            - Avoid conflicts of interest; disclose any potential conflicts to your manager
            - Never offer or accept bribes, kickbacks, or improper payments

            ### 2. Respect
            - Treat all colleagues, customers, and partners with dignity and respect
            - Embrace diversity and foster an inclusive workplace
            - Zero tolerance for harassment, discrimination, or bullying of any kind

            ### 3. Confidentiality
            - Protect proprietary information, trade secrets, and customer data
            - Do not discuss confidential matters in public spaces
            - Use secure channels for sensitive communications

            ## Reporting Violations
            If you witness or suspect a violation of this Code:
            1. Report to your direct manager
            2. Contact HR Business Partner
            3. Use the **anonymous Ethics Hotline**: 1800-CONTOSO

            > **Note**: Retaliation against anyone who reports concerns in good faith is strictly prohibited and will result in disciplinary action.

            ## Acknowledgement
            All employees must acknowledge this Code annually via the HR portal.

            ---
            *Last reviewed: August 2025 | Next review: August 2026*
            """),
        new Policy(
            2,
            "Remote Work Policy",
            "Workplace",
            DateTime.Now.AddMonths(-2),
            "Lisa Thompson",
            "Contoso has a new remote work policy. Familiarise yourself with the new rules to learn whether you are eligible.",
            """
            # Remote Work Policy

            ## Eligibility
            Remote work arrangements are available to employees who:
            - Have completed their probationary period (minimum 3 months)
            - Maintain a performance rating of "Meets Expectations" or above
            - Have a role suitable for remote execution (as determined by department head)

            ## Work Arrangements

            | Arrangement | Office Days | Remote Days | Eligibility |
            |-------------|-------------|-------------|-------------|
            | Hybrid | 2-3 days | 2-3 days | All eligible employees |
            | Fully Remote | 0 days | 5 days | Approval required |
            | Office-Based | 5 days | 0 days | Default arrangement |

            ## Requirements

            ### Home Office Setup
            - Dedicated workspace with appropriate ergonomics
            - Reliable internet connection (minimum 50 Mbps)
            - Contoso-approved security software installed
            - Webcam and microphone for video conferencing

            ### Availability
            - Core hours: **10:00 AM - 3:00 PM AEST** (must be available)
            - Respond to messages within 2 hours during work hours
            - Camera on for team meetings unless bandwidth issues

            ## Expenses
            Contoso provides a one-time **$500 home office allowance** for:
            - Ergonomic chair or standing desk
            - Monitor and peripherals
            - Lighting and accessories

            ## Changing Arrangements
            Submit requests through the HR portal with **14 days notice**. Manager approval required.

            ---
            *Questions? Contact your HR Business Partner or email hr@contoso.com*
            """),
        new Policy(
            3,
            "Data Protection & Privacy",
            "Security",
            DateTime.Now.AddMonths(-4),
            "Aisha Rahman",
            "All company and customer data must be handled in accordance with applicable regulations. New controls and guidelines are now in effect.",
            """
            # Data Protection & Privacy Policy

            ## Overview
            Contoso is committed to protecting personal data in compliance with:
            - Australian Privacy Act 1988
            - GDPR (for EU customers/employees)
            - Industry-specific regulations (HIPAA, PCI-DSS where applicable)

            ## Data Classification

            | Classification | Description | Examples | Handling |
            |----------------|-------------|----------|----------|
            | **Public** | No restrictions | Marketing materials, press releases | Open sharing |
            | **Internal** | Business use only | Org charts, internal memos | Internal systems only |
            | **Confidential** | Restricted access | Customer data, financials | Encrypted, access-controlled |
            | **Highly Confidential** | Strictly controlled | PII, health data, credentials | MFA, audit logging, encryption |

            ## Key Requirements

            ### Data Collection
            - Only collect data necessary for stated business purposes
            - Obtain explicit consent where required
            - Provide clear privacy notices

            ### Data Storage
            - Store in approved systems only (see *Approved Tools List*)
            - Enable encryption at rest and in transit
            - **Never** store sensitive data in email, Slack, or personal devices

            ### Data Retention
            ```
            Customer Data:     7 years after relationship ends
            Employee Records:  7 years after employment ends
            Financial Records: 7 years
            Marketing Data:    Until consent withdrawn
            ```

            ## Incident Response
            If you suspect a data breach:
            1. **Do not** attempt to investigate yourself
            2. Report immediately to security@contoso.com
            3. Preserve any evidence
            4. Do not discuss externally

            > ⚠️ **Warning**: Data breaches must be reported to authorities within 72 hours. Time is critical.

            ---
            *Security team contact: security@contoso.com | Emergency: +61 2 9456 3999*
            """),
        new Policy(
            4,
            "Leave Policy",
            "HR",
            DateTime.Now.AddMonths(-8),
            "Michelle Torres",
            "Understand your rights and entitlements, and obligations, with regard to leave.",
            """
            # Leave Policy

            ## Annual Leave
            - **Entitlement**: 20 days per year (pro-rata for part-time)
            - **Accrual**: 1.67 days per month
            - **Carry-over**: Maximum 10 days to next year
            - **Notice**: 2 weeks for 1-5 days; 4 weeks for 5+ days

            ## Personal/Carer's Leave
            - **Entitlement**: 10 days per year
            - **Use**: Personal illness or caring for immediate family
            - **Evidence**: Medical certificate required for 2+ consecutive days

            ## Other Leave Types

            | Leave Type | Entitlement | Paid | Notes |
            |------------|-------------|------|-------|
            | Parental (Primary) | 16 weeks | ✓ | Plus government payments |
            | Parental (Secondary) | 4 weeks | ✓ | Within 12 months of birth |
            | Compassionate | 3 days per occasion | ✓ | Death or serious illness of family |
            | Family Violence | 10 days | ✓ | Confidential; contact HR directly |
            | Jury Duty | As required | ✓ | Provide court documentation |
            | Study Leave | 5 days/year | ✓ | Pre-approved courses only |

            ## Public Holidays
            Contoso observes all national and state public holidays. Employees required to work on public holidays receive **time and a half** or a day in lieu.

            ## How to Apply
            1. Submit request in **Workday** at least 2 weeks in advance
            2. Obtain manager approval
            3. Ensure coverage arrangements are documented

            ## Long Service Leave
            After **7 years** of continuous service: 8.67 weeks paid leave.

            ---
            *For complex leave situations, contact your HR Business Partner.*
            """),
        new Policy(
            5,
            "Expense Reimbursement",
            "Finance",
            DateTime.Now.AddMonths(-3),
            "James Thornton",
            "Guidelines for submitting business expenses for reimbursement including travel, meals, and equipment.",
            """
            # Expense Reimbursement Policy

            ## General Principles
            - Expenses must be **reasonable, necessary, and business-related**
            - Submit within **30 days** of incurring the expense
            - **Original receipts required** for all claims over $25

            ## Approval Thresholds

            | Amount | Approver |
            |--------|----------|
            | Up to $500 | Direct Manager |
            | $500 - $2,000 | Department Head |
            | $2,000 - $10,000 | VP/Director |
            | Over $10,000 | CFO |

            ## Travel Expenses

            ### Flights
            - **Domestic**: Economy class
            - **International (<6 hours)**: Economy class
            - **International (>6 hours)**: Premium economy (business class requires CFO approval)

            ### Accommodation
            - Maximum **$250/night** in major cities
            - Maximum **$180/night** in regional areas
            - Book through corporate travel portal for preferred rates

            ### Meals (Per Diem)
            ```
            Breakfast:  $25
            Lunch:      $30
            Dinner:     $50
            Daily Max:  $105
            ```

            ## Non-Reimbursable Expenses
            - 🚫 Alcohol (unless client entertainment, pre-approved)
            - 🚫 Personal travel extensions
            - 🚫 Gym or spa services
            - 🚫 Traffic fines or parking tickets
            - 🚫 Upgrades without approval

            ## Submission Process
            1. Complete expense report in **Concur**
            2. Attach photos/scans of all receipts
            3. Include business purpose for each item
            4. Submit for approval

            > **Tip**: Use the Concur mobile app to capture receipts in real-time!

            ---
            *Questions? Contact finance@contoso.com*
            """),
        new Policy(
            6,
            "Information Security",
            "Security",
            DateTime.Now.AddMonths(-1),
            "Aisha Rahman",
            "Policies and procedures to protect Contoso's information assets and systems from threats.",
            """
            # Information Security Policy

            ## Password Requirements
            All passwords must meet these criteria:
            - Minimum **14 characters**
            - Mix of uppercase, lowercase, numbers, and symbols
            - **No reuse** of last 12 passwords
            - Changed every **90 days** (or immediately if compromised)

            ## Multi-Factor Authentication (MFA)
            **Required for:**
            - All cloud applications (Microsoft 365, Salesforce, etc.)
            - VPN access
            - Administrative systems
            - Code repositories

            ## Device Security

            ### Company Devices
            - Full disk encryption enabled
            - Automatic screen lock after 5 minutes
            - Antivirus/EDR software installed
            - Automatic updates enabled

            ### Personal Devices (BYOD)
            | Allowed | Not Allowed |
            |---------|-------------|
            | Email (via approved app) | Storing company files |
            | Calendar access | Admin/developer access |
            | Teams/Slack | Financial systems |

            ## Acceptable Use
            **Do:**
            - ✅ Use company resources for business purposes
            - ✅ Report suspicious emails to security@contoso.com
            - ✅ Lock your screen when away from desk
            - ✅ Use VPN when on public networks

            **Don't:**
            - ❌ Share credentials with anyone
            - ❌ Install unauthorized software
            - ❌ Connect untrusted USB devices
            - ❌ Send sensitive data via personal email

            ## Incident Reporting
            Report security incidents **immediately**:
            - 📧 Email: security@contoso.com
            - 📞 Phone: +61 2 9456 3999
            - 🚨 Critical: Page on-call via PagerDuty

            ---
            *Mandatory security training must be completed annually.*
            """)
    ];

    private static readonly List<Accreditation> Accreditations =
    [
        new Accreditation(1,
            "Health and Safety Certificate",
            "Employees with this accreditation are permitted to work from any Contoso site, as well as their home address upon agreement. Covers workplace hazards, emergency procedures, and ergonomic best practices. Renewal required annually.",
            "Overdue",
            "Internal",
            DateTime.Now.AddDays(-45)),
        new Accreditation(2,
            "Accounting Level 1",
            "Required for all staff wishing to work in Contoso's Finance department. Covers financial controls, audit procedures, and compliance requirements specific to Contoso's operations.",
            "Pending",
            "Internal",
            DateTime.Today.AddDays(14)),
        new Accreditation(3,
            "People Leadership",
            "This accreditation signifies that an employee has met the requirements to hold a management level position at Contoso. Includes training on performance management, difficult conversations, and inclusive leadership.",
            "Valid",
            "Internal",
            DateTime.Today.AddMonths(6)),
        new Accreditation(4,
            "Advanced Security Clearance",
            "Required for employees working with highly confidential client data or government contracts. Includes background check and ongoing monitoring requirements.",
            "Valid",
            "External",
            DateTime.Today.AddMonths(24)),
        new Accreditation(5,
            "First Aid Officer",
            "Certified to provide first aid response in workplace emergencies. Holder is designated as floor warden and must maintain CPR certification.",
            "Valid",
            "External",
            DateTime.Today.AddMonths(12)),
        new Accreditation(6,
            "Fire Warden",
            "Trained to lead evacuation procedures and operate fire safety equipment. Required for at least one employee per floor/department.",
            "Valid",
            "Internal",
            DateTime.Today.AddMonths(18))
    ];

    public Task<IEnumerable<CompanyUpdate>> GetCompanyUpdatesAsync()
    {
        return Task.FromResult<IEnumerable<CompanyUpdate>>(Updates);
    }

    public Task<CompanyUpdate?> GetCompanyUpdateAsync(int id)
    {
        return Task.FromResult(Updates.FirstOrDefault(u => u.UpdateId == id));
    }

    public Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        return Task.FromResult<IEnumerable<Employee>>(Employees);
    }

    public Task<Employee?> GetEmployeeAsync(int id)
    {
        return Task.FromResult(Employees.FirstOrDefault(e => e.EmployeeId == id));
    }

    public Task<IEnumerable<Policy>> GetPoliciesAsync()
    {
        return Task.FromResult<IEnumerable<Policy>>(Policies);
    }

    public Task<Policy?> GetPolicyAsync(int id)
    {
        return Task.FromResult(Policies.FirstOrDefault(p => p.PolicyId == id));
    }

    public Task<IEnumerable<Accreditation>> GetAccreditationsAsync()
    {
        return Task.FromResult<IEnumerable<Accreditation>>(Accreditations);
    }

    public Task<Accreditation?> GetAccreditationAsync(int id)
    {
        return Task.FromResult(Accreditations.FirstOrDefault(a => a.AccreditationId == id));
    }
}
