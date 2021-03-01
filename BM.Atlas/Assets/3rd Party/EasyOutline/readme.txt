# Read Me

## Introduction
Thank you for purchasing Easy Outline 2.0
In this document you can find a brief getting started section along with a small FAQ and contact details, should you have any queries.

## Getting Started
It's now easier than ever to setup Easy Outline in your project.
Simply add the EasyOutlineSystem component (_./EasyOutline/Behaviours/EasyOutlineSystem.cs_) to the main camera in your scene.
Now, you can add the renderers of the game objects that you want to have outlined into the *rendererList* array of the EasyOutlineSystem component.

## Customisation
Now that you are up and running with Easy Outline, you can further customise your outlines.
These are the parameters which will impact the look and performance of your outlines:
*outlineMode* - Switches between outline modes:
    * *None* - No outline.
    * *Culled* - The outline is occluded by foreground objects.
    * *Inverted* - Inverts the culled outline.
*fillMode* - Switches between fill modes:
    * *None* - No fill.
    * *Culled* - The fill is occluded by foreground objects.
    * *Inverted* - Inverts the culled fill.
*outlineColor* - Changes the colour of the outline.
*fillColor* - Changes the colour of the fill.
*outlineThickness* - Choose how thick the outlines appear (size is calculated in screen size, regardless of object distance).
*sampleCount* - Changes the number of samples taken to calculate the outline (more samples equals a smoother outline).

## Performance
In order to keep performance high, there are a few key things to adhere to and consider:
-   Keep the number of EasyOutlineSystem components to a minimum. Each component requires an additional CommandBuffer, which is the highest cost of this approach.
-   The number of game objects assigned to an EasyOutlineSystem component has negligible impact on the performance. Group all objects on one EasyOutlineSystem component for the best performance.
-   Sample count can be reduced when outline thickness is reduced.

## FAQ
Q. Does this asset work with VR?
A. Yes! The asset has been tested with Multi-Pass and Single-Pass. If youhave any problems please send me an email with details and I will endeavour to fix them!

Q. Does this asset work with the post-processing stack?
A. Yes! This is a major improvement on the original Easy Outline. However, due to a limitation of Unity, the post processing stack must be applied to a secondary child camera of the main camera, with 'Nothing' set as the culling mask. This has no negative impact on performance. (See the demo scene for an example setup)

Q. Does this asset work for 2D games as well as 3D games?
A. Yes! Anything with a renderer can be outlined!

Q. Why should I pay for this asset when there are free alternatives on the asset store?
A: Most assets out there use mesh-vertex-expansion techniques which, although fast, do not provide an aesthetically pleasing outline on uneven shapes (not a cube/sphere). In addition to this, Easy Outline will work with whatever you decide to use it for in the future, 2D Sprites, 3D models, Terrain, VR, AR etc.

Q. If I have a problem, can you help?
A. I will try! I am just a solo developer with a full-time job, so, unfortunately, I can't answer support emails as quick as I'd like. However, if you have a query or a problem that isn't mentioned here or in the comments, please do send me an answer and I will reply as soon as possible!

Q. It works in Editor, but doesn't work when built!
A. Ensure that the shaders located in *EasyOutline/Shaders/* are added to the "Always Included Shaders" list (Build Settings -> Player Settings -> Graphics -> Always Included Shaders).

Q. The outline is behaving strange on X device, how can I fix it?
A. Due to differences in Shader behaviour on devices, there may be a scenario that has not been tested and produces undesired results. If you find such a hardware combination, please contact me with details of the target platform and a screenshot of the results.

Q. We used this in our game, do you want to see?
A: YES! Helping developers is the reason I make assets. I love you hear stories of games (published or unpublished) that make use of Easy Outline. Please send me an email with screenshots/videos!

## Author
[David Dunnings](http://www.daviddunnings.com)
Email: [mrdaviddunnings@gmail.com](mailto:mrdaviddunnings@gmail.com)