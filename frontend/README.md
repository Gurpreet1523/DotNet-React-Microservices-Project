# Portfolio Frontend (React + Vite)

React SPA for the `portfolio-solution` backend. Talks to nothing but the **Ocelot Gateway**
(`Portfolio.Gateway`, `:5000`) — the gateway is the only base URL the frontend knows about;
Ocelot re-routes internally to Auth / Profile / Projects / Skills / Contact.

## Run it

```bash
npm install
npm run dev        # http://localhost:5173, proxies /api -> http://localhost:5000
npm run build       # production build to dist/
```

`vite.config.js` proxies `/api/*` to the gateway in dev, so `.env`'s `VITE_API_BASE_URL` can stay
empty locally. Set it to the deployed gateway URL for production (e.g.
`VITE_API_BASE_URL=https://api.yourdomain.com`).

## Folder structure

```
src/
├── api/            one file per downstream service (httpClient, authService, profileService,
│                    projectsService, skillsService, contactService, healthService)
├── context/         AuthContext — session state derived from the JWT
├── hooks/           useFetch — shared loading/error/data hook, used by every component
├── components/
│   ├── layout/       Navbar, Footer
│   ├── common/       Loader, ErrorMessage
│   ├── auth/         LoginForm, ProtectedRoute
│   ├── home/         Hero, ExperienceTimeline, ServiceTopology (live health strip)
│   ├── projects/     ProjectCard, ProjectGrid
│   ├── skills/       SkillBadge, SkillsGrid
│   └── contact/       ContactForm
├── pages/            one per route (Home, Projects, ProjectDetail, Skills, Contact, Login,
│                     AdminDashboard, NotFound)
├── routes/           AppRoutes.jsx — central React Router config
└── App.jsx / main.jsx
```

**Rule the whole app follows:** components never call axios/fetch directly. They call a
function from `api/*Service.js` through the `useFetch` hook. This means swapping the HTTP
library, adding retry logic, or moving to GraphQL later touches one folder, not forty
components.

## Gateway routing this app assumes

Configure `Portfolio.Gateway`'s `ocelot.json` so these upstream paths exist. Downstream ports
match your solution layout:

| Frontend calls              | Gateway routes to                          |
|------------------------------|---------------------------------------------|
| `POST /api/auth/login`       | `Portfolio.Auth.API` (:5001) `/login`        |
| `POST /api/auth/register`    | `Portfolio.Auth.API` `/register`             |
| `POST /api/auth/refresh`     | `Portfolio.Auth.API` `/refresh`              |
| `GET  /api/auth/me`          | `Portfolio.Auth.API` `/me`                   |
| `GET  /api/profile`          | `Portfolio.Profile.API` (:5002) `/profile`   |
| `PUT  /api/profile`          | `Portfolio.Profile.API` `/profile`           |
| `GET  /api/profile/experience` | `Portfolio.Profile.API` `/experience`      |
| `GET  /api/profile/education`  | `Portfolio.Profile.API` `/education`       |
| `GET  /api/projects`         | `Portfolio.Projects.API` (:5003) `/projects` |
| `GET  /api/projects/{id}`    | `Portfolio.Projects.API` `/projects/{id}`    |
| `POST/PUT/DELETE /api/projects...` | same, admin-only (JWT required)        |
| `GET  /api/skills`           | `Portfolio.Skills.API` (:5004) `/skills`     |
| `POST /api/contact`          | `Portfolio.Contact.API` (:5005) `/contact`   |
| `GET  /api/contact`          | `Portfolio.Contact.API` `/contact` (admin-only) |
| `GET  /health` + `/api/*/health` | each service's health endpoint          |

Example Ocelot route block (repeat per service, adjust ports/paths):

```json
{
  "UpstreamPathTemplate": "/api/profile/{everything}",
  "UpstreamHttpMethod": [ "Get", "Put", "Post" ],
  "DownstreamPathTemplate": "/{everything}",
  "DownstreamScheme": "http",
  "DownstreamHostAndPorts": [ { "Host": "localhost", "Port": 5002 } ]
}
```

## Expected DTO shapes

The frontend maps these fields directly — keep your `Portfolio.Shared.Contracts` DTOs aligned,
or adjust the `api/*Service.js` mapping if your field names differ.

**ProfileDto** (`GET /api/profile`)
```json
{ "fullName": "string", "summary": "string", "resumeUrl": "string" }
```

**ExperienceDto[]** (`GET /api/profile/experience`)
```json
[{ "id": 1, "company": "string", "role": "string", "startDate": "2023-01-01",
   "endDate": null, "highlights": ["string"] }]
```

**ProjectDto[]** (`GET /api/projects`)
```json
[{ "id": 1, "title": "string", "shortDescription": "string", "year": 2025, "tags": ["C#"] }]
```

**ProjectDetailDto** (`GET /api/projects/{id}`) — same as above plus:
```json
{ "description": "string", "repoUrl": "string", "liveUrl": "string" }
```

**SkillDto[]** (`GET /api/skills`)
```json
[{ "id": 1, "name": "ASP.NET Core", "category": "Backend", "proficiency": 90 }]
```

**ContactMessageDto** (`POST /api/contact` body / `GET /api/contact` response)
```json
{ "name": "string", "email": "string", "message": "string" }
```

**AuthResponseDto** (`POST /api/auth/login`)
```json
{ "accessToken": "jwt", "refreshToken": "string" }
```

## Auth flow

1. `LoginForm` calls `authService.login()` → stores `accessToken`/`refreshToken` in
   `localStorage` → `AuthContext` calls `/api/auth/me` to hydrate the user.
2. `httpClient`'s request interceptor attaches `Authorization: Bearer <token>` to every call.
3. On any `401`, the response interceptor clears tokens and fires a `auth:unauthorized` window
   event; `AuthContext` listens for it and logs the user out reactively.
4. `ProtectedRoute` guards `/admin`, redirecting to `/login` and preserving the original
   destination via router state.

Swap `localStorage` for httpOnly cookies + a `/refresh` endpoint call on 401 if you want to
harden this beyond a portfolio-project threshold.

## Design system

Dark ink-blue base (`--color-bg: #0b1120`), single amber accent (`--color-accent: #f5a623`),
green/red reserved only for service status. Space Grotesk for display type, Inter for body,
JetBrains Mono for labels/data — reflecting the code-first subject matter. All tokens live in
`src/styles/tokens.css`.

The signature element is `ServiceTopology.jsx` on the home page: it pings every microservice's
`/health` route through the gateway every 30s and renders a live online/offline strip — the
frontend visibly reflects the actual distributed backend instead of hiding it.
