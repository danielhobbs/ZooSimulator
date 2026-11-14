### Zoo Simulator

Built as a .NET 8 Web API with a plain HTML and JavaScript page.

#### Running it

Requires the .NET SDK (8 or later).

```
dotnet run --project ZooSimulator.Api --launch-profile https
```
Then open **https://localhost:7184** (or press F5 in Visual Studio).

#### Approach
I purposely focussed on the back end - services presented through an API, with a light front end - as I understood API microservices to be the main focus of the role, so I put the effort into how the services are designed rather than into front-end logic and aesthetics.

Mapped out offline first then digital, see 2 images in docs folder:

- [UI wireframe](docs/Planning_1_UI_Wireframe.jpg)
- [Workflow mapping](docs/Planning_2_Workflow_Mapping.jpg)

I then coded test-first for the core rules (stub, failing test, implementation), and added tests alongside the code for the rest, committing frequently to show how things were built up to the final product.

#### Notes
Purposefully targeting .NET 8 to allow for anyone not on 10 to run for now.

.NET 8 left support on 10 November 2026, so this can always be upgraded to 10, which is a small change.