import 'dart:io' as io;
import 'dart:io';

import 'package:camera/camera.dart';
import 'package:flutter/material.dart';
import 'package:path/path.dart';
import 'package:path_provider/path_provider.dart';

import '../piaggio/detail_model_screen.dart';

class CameraPage extends StatefulWidget {
  final List<CameraDescription>? cameras;
  const CameraPage({this.cameras, Key? key}) : super(key: key);

  @override
  _CameraPageState createState() => _CameraPageState();
}

class _CameraPageState extends State<CameraPage> {
  late CameraController controller;
  var pictureFile;

  @override
  void initState() {
    super.initState();
    controller = CameraController(
      widget.cameras![0],
      ResolutionPreset.max,
    );
    controller.initialize().then((_) {
      if (!mounted) {
        return;
      }
      setState(() {});
    });
  }

  @override
  void dispose() {
    controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    if (!controller.value.isInitialized) {
      return const SizedBox(
        child: Center(
          child: CircularProgressIndicator(),
        ),
      );
    }
    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.all(8.0),
          child: Center(
            child: SizedBox(
              height: 400,
              width: 400,
              child: CameraPreview(controller),
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(8.0),
          child: ElevatedButton(
            onPressed: () async {
              Directory tempDir = await getTemporaryDirectory();

              // Construct the path where the image should be saved using the
              // pattern package.
              final path = join(
                // Store the picture in the temp directory.
                // Find the temp directory using the `path_provider` plugin.
                (await getTemporaryDirectory()).path,
                '${DateTime.now()}.png',
              );

              XFile t = await controller.takePicture();
              t.saveTo(path);
              pictureFile = path;
              GallerySaver.saveImage(path)

              setState(() {});
            },
            child: const Text('Capture Image'),
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(8.0),
          child: ElevatedButton(
            onPressed: () async {},
            child: const Text('Back To Home Screen'),
          ),
        ),
        if (pictureFile != null && pictureFile.length > 0)
          Image.network(
            pictureFile,
            height: 200,
          )
        //Android/iOS
        // Image.file(File(pictureFile!.path)))
      ],
    );
  }
}
