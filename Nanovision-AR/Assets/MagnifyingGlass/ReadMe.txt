Thank you for downloading Magnifying Glass package !
This package will bring an amazing "magnifying glass" effect to your game.

Magnifying effect is designed as a fullscreen postprocess effect.
The shader has two exposed input parameters.
  1. _CenterRadial ( Vector4 )
     xy component means the center position of the magnifying glass.
	 zw component means the radial length of the magnifying glass. 
	 
  2. _Amount ( float )
     means the intensity of magnifying effect.

The script "MagnifyingGlass.cs" is a camera script for demonstrate effect.
It implements a tiny UI so you can tweak the effect through it, and make the magnifying glass follow our mouse when you press left button.

=======================================================================================================================================================
Version 1.2 Update
  Implement rectangular magnifying glass effect !
  The script "MagnifyingGlassQuad.cs" is the rectangular magnifying glass behaviour.
  Try demo "DemoQuadGlass.unity" to see the effect.

=======================================================================================================================================================
Version 1.5 Update
  Implement volumetric sphere magnifying glass effect !
  The script "MagnifyingGlassVolumetricSphere.cs" and shader "Magnifying Glass/Volumetric Sphere" are the new magic.
  Try demo "DemoVolumetricSphereGlass.unity" to see the effect.

=======================================================================================================================================================
Version 1.6 Update
  Complicated circle magnifying glass effect support radial material parameters.
  Complicated circle magnifying glass effect support multiple glass.
  Fix Y flip problem when unity build-in AA is enable.

=======================================================================================================================================================
Version 1.8 Update
  Dark / bright color for non-magnifying / magnifying part of screen.

=======================================================================================================================================================
Version 1.9 Update
  Now rendering pipeline support any number of "magnifying glass" on screen.
  Please open "DemoGlassNew.unity" to see our new rendering approach.

=======================================================================================================================================================
Version 2.0 Update
  Support 2D sprite and UI Image rendering.
  Open "DemoSpriteAndImage.unity" to see the new effects.

If you like it, please give it a good review on asset store. Thanks so much !
Any suggestion or improvement you want, please contact qq_d_y@163.com.
Hope we can help more and more game developers.