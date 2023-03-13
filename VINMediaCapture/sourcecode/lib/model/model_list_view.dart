import 'package:VinMediaCapture/app_theme.dart';
import 'package:VinMediaCapture/hotel_booking/hotel_app_theme.dart';
import 'package:VinMediaCapture/objectmodel/enum.dart';
import 'package:camera/camera.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:path/path.dart';
import '../camera/camera_page.dart';
import '../login/Toast.dart';
import 'doc_type_model_list_data.dart';
import 'dart:async';
import 'dart:io';

class ModelListView extends StatelessWidget {
  const ModelListView(
      {Key? key,
      this.modelData,
      this.animationController,
      this.animation,
      this.callback})
      : super(key: key);

  final VoidCallback? callback;
  final DocTypeModelListData? modelData;
  final AnimationController? animationController;
  final Animation<double>? animation;

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: animationController!,
      builder: (BuildContext context, Widget? child) {
        return FadeTransition(
          opacity: animation!,
          child: Transform(
            transform: Matrix4.translationValues(
                0.0, 50 * (1.0 - animation!.value), 0.0),
            child: Padding(
              padding: const EdgeInsets.only(
                  left: 24, right: 24, top: 8, bottom: 16),
              child: InkWell(
                splashColor: Colors.transparent,
                onTap: callback,
                child: Container(
                  decoration: BoxDecoration(
                    borderRadius: const BorderRadius.all(Radius.circular(16.0)),
                    boxShadow: <BoxShadow>[
                      BoxShadow(
                        color: Colors.grey.withOpacity(0),
                        offset: const Offset(4, 4),
                        blurRadius: 16,
                      ),
                    ],
                  ),
                  child: ClipRRect(
                    borderRadius: const BorderRadius.all(Radius.circular(0)),
                    child: Stack(
                      children: <Widget>[
                        Column(
                          children: <Widget>[
                            Container(
                              color: HotelAppTheme.buildLightTheme()
                                  .backgroundColor,
                              child: Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: <Widget>[
                                  Expanded(
                                    child: Container(
                                      child: Padding(
                                        padding: const EdgeInsets.only(
                                            left: 16, top: 8, bottom: 8),
                                        child: Column(
                                          mainAxisAlignment:
                                              MainAxisAlignment.center,
                                          crossAxisAlignment:
                                              CrossAxisAlignment.start,
                                          children: <Widget>[
                                            Text(
                                              modelData!.titleTxt,
                                              textAlign: TextAlign.left,
                                              style: TextStyle(
                                                fontWeight: FontWeight.w600,
                                                fontSize: 22,
                                              ),
                                            ),
                                            Row(
                                              crossAxisAlignment:
                                                  CrossAxisAlignment.center,
                                              mainAxisAlignment:
                                                  MainAxisAlignment.start,
                                              children: <Widget>[
                                                Text(
                                                  modelData!.subTxt,
                                                  style: TextStyle(
                                                      fontSize: 14,
                                                      color: Colors.grey
                                                          .withOpacity(0.8)),
                                                ),
                                                const SizedBox(
                                                  width: 4,
                                                )
                                              ],
                                            ),
                                            Row(
                                              crossAxisAlignment:
                                                  CrossAxisAlignment.center,
                                              mainAxisAlignment:
                                                  MainAxisAlignment.start,
                                              children: <Widget>[
                                                Text(
                                                  modelData!.errorValidate,
                                                  style: TextStyle(
                                                      fontSize: 14,
                                                      color: Colors.red
                                                          .withOpacity(0.8)),
                                                ),
                                                const SizedBox(
                                                  width: 4,
                                                )
                                              ],
                                            ),
                                          ],
                                        ),
                                      ),
                                    ),
                                  )
                                ],
                              ),
                            ),
                            controlWidget(context),
                          ],
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          ),
        );
      },
    );
  }

  Widget controlWidget(BuildContext context) {
    TextEditingController txtText = new TextEditingController();
    txtText.text = modelData!.textValue;
    if (modelData?.attrDocType == EAttrDataType.IMGCAPT) {
      var widget = new GestureDetector(
        // When the child is tapped, show a snackbar.
        onTap: () async {
          showInformationDialog(context);
          /*
          //Bật camera. Code chuẩn
          var cameras = await availableCameras().then(
            (value) => Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) => CameraPage(
                  cameras: value,
                ),
              ),
            ),
          );*/
          var a = 1;
        },
        // The custom button
        child: AspectRatio(
          aspectRatio: 2,
          child: modelData?.assetImage.isNotEmpty == true
              ? Image.file(File(modelData!.assetImage))
              : Image.network(
                  modelData!.imagePath,
                  fit: BoxFit.fill,
                ),
        ),
      );
      return widget;
    } else if (modelData?.attrDocType == EAttrDataType.VARCHAR) {
      var widget = new TextField(
          controller: txtText,
          maxLines: null,
          onChanged: (String txt) {
            modelData?.textValue = txt;
          },
          style: TextStyle(
            fontFamily: AppTheme.fontName,
            fontSize: 16,
            color: AppTheme.lightText,
          ),
          cursorColor: Colors.blue,
          decoration: InputDecoration(
              border: OutlineInputBorder(), hintText: 'Nhập giá trị...'));

      return widget;
    }
    return Text("data");
  }

  Future<void> showInformationDialog(BuildContext context) async {
    var value = await availableCameras();
    return await showDialog(
        context: context,
        builder: (context) {
          final TextEditingController _textEditingController =
              TextEditingController();
          bool isChecked = false;
          return StatefulBuilder(builder: (context, setState) {
            return AlertDialog(
              content: Form(
                  key: key,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      CameraPage(
                        cameras: value,
                        modelData: modelData,
                      ),
                    ],
                  )),
              actions: <Widget>[
                TextButton(
                  child: Text('Okay'),
                  onPressed: () {
                    //if(key.currentState!.validate()){
                    // Do something like updating SharedPreferences or User Settings etc.
                    Navigator.of(context).pop();
                    // }
                  },
                ),
              ],
            );
          });
        });
  }
}
