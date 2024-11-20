## SimpleCamera 2.2
+ Fixed  When selecting the ParameterConfiguration object, there is a problem of instant transition when drawing the camera closer

## SimpleCamera 2.1
+ Fixed  Time.timeScale changed  the camera is also affected,Use updata and Time.unscaledDeltaTime solution
+ Fixed  Fix fast rotation interpolation exception,Solve the deadlock problem of universal joint
+ Fixed the problem that the camera does not follow when focusing on moving objects
+ Rewriting the focus algorithm, you can now correctly calculate the weight proportion according to the size of the object
+ Fixed the conflict between the rotation and scaling of the mobile end, which caused the scaling to suddenly become 0 degrees

## SimpleCamera 2.0
+ Fixed  Adaptive translation speed and added correction function to adapt to devices with different sensitivities
+ Fixed  Adaptive scaling speed and added correction function to adapt to devices with different sensitivities
+ Add mandatory settings when setting the camera position, and there is no transition animation  
  SetCameraPostionRotation(Vector3 positon, Vector3 rotation,bool isForce=false)
+ Add method of setting movable area of target and camera  
  SetAimAndCameraClamp(Vector3 centerPos, Vector3 aimBoxExtend, Vector3 cameraBoxExtend)
+ Add Method of setting the maximum and minimum distance between the target and the camer
  SetAimAndCameraDistance(float minDis=1,float MaxDis=50)
+ Single module control methods, such as turning on rotation only, turning off translation, turning off zoom, 
  using keyboard translation only, using mouse translation only, etc
  EnablePaning(bool enablekeybordInput=true,bool enableMouseInput=true,bool useScreenEdgeInput=true)
  EnableRotate(bool enableMouseRotation=true)
  EnableZoom(bool enableScrollwheelZooming=true,bool enableKeybordZooming=true)
  
## SimpleCamera 1.3
+ Fixed  the problem of translation reset after zooming on the mobile terminal
+ Fixed adaptive zoom to 0 Bug

## SimpleCamera 1.2
+ New adaptive translation speed,Short distance translation is slow, long distance translation is fast
+ New adaptive zoom speed,Fast long-distance zoom, slow short-distance zoom, suitable for viewing targets close
+ Fixed adaptive translation speed near distance translation speed is 0 Bug

## SimpleCamera 1.1
+ First release