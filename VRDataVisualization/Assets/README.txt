VR Data Visualization is a Unity project that is meant to explore how the Oculus Rift can be used to visualize different kinds of data. In order to use the visualization, you will need an Oculus Rift and 2 Oculus Touch controllers. This project was developed using the Oculus SDK (OVR) and 

Any GameObject that is made controllable can be manipulated with the Oculus Touch controllers. All manipulation instructions are shown in the scene on a UI panel, which can be activated and deactivated with the controllers.

Included in the scene hierarchy are a couple objects which are set up to already be able to be manipulated or "controlled". These include a simple cube, a fly brain model, a particle of several thousand atoms, and an object that can generate point clouds from .csv/.ply files. In order to achieve a higher FPS, it is advised to only enable one object at once.

All custom controller bindings are written up in controller.cs and canvascontroller.cs.