# Blazor Fuel Economy
This is a Blazor WebAssembly project that demonstrates how to use the Blazor WebAssembly framework to create a tool that manages fuel economy data for multiple vehicles.

## Features
- Offline support
- Hosted on GitHub Pages

## Notes
1. Because GitHub Pages hosts the site in a subdirectory, the base URL must be set in the `index.html` file before deploying the site. This must be done prior to building the site otherwise you will get integrity failures from the service worker.
2. Sample data is in the repository (`sample data.csv`), but must be imported from the **Entries** tab after creating a vehicle.

## Useful Resources
1. https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly - General documentation for deploying Blazor WASM.
2. https://github.com/rafrex/spa-github-pages - For better handling of single-page-applications on GitHub Pages
