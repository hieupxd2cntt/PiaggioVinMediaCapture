import 'dart:convert';

import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/app_theme.dart';
import 'package:VinMediaCapture/barcode/scanbarcode.dart';
import 'package:VinMediaCapture/hotel_booking/hotel_app_theme.dart';
import 'package:VinMediaCapture/login/Toast.dart';
import 'package:VinMediaCapture/login/loginscreen.dart';
import 'package:VinMediaCapture/objectmodel/DocTypeItemAddModel.dart';
import 'package:VinMediaCapture/objectmodel/enum.dart';
import 'package:VinMediaCapture/piaggio/barcode_scan_screen.dart';
import 'package:VinMediaCapture/piaggio/detail_model_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';

int ViewVincode = 0;
TextEditingController txtTotalAttribute = new TextEditingController();
TextEditingController txtModel = new TextEditingController();

class VinCodeScanScreen extends StatefulWidget {
  @override
  _VinCodeScanScreenState createState() => _VinCodeScanScreenState();
}

class ViewVinCodeScanScreen extends StatefulWidget {
  @override
  _VinCodeScanScreenState createState() => _VinCodeScanScreenState();
}

class _VinCodeScanScreenState extends State<VinCodeScanScreen> {
  TextEditingController txtVinCode = new TextEditingController();
  @override
  void initState() {
    txtVinCode.text = "";
    var sessionManager = SessionManager();
    sessionManager
        .get("currentSession")
        .then((value) => currentSessionText = value);
    sessionManager
        .get("currentBarcode")
        .then((value) => ShowInfoByBarcode(value));
    super.initState();
  }

  Future<void> ShowInfoByBarcode(var barcode) async {
    var getListAttribute = await GetListAttribute(barcode);
    List<DocTypeItemAddModel> attrs = [];
    var tagObjsJson = jsonDecode(getListAttribute.body) as List;
    if (tagObjsJson.length == 0) {
      toastmessage("Không có thuộc tính được cấu hình cho barcode này.");
      txtModel.text = "";
      txtTotalAttribute.text = "";
      setState(() {});
      return;
    } else {
      try {
        attrs = tagObjsJson
            .map((tagJson) => DocTypeItemAddModel.fromJson(tagJson))
            .toList();
        txtModel.text = "Thông tin xe :" +
            (attrs[0].model == null ? "" : attrs[0].model!.modelCode!);
        var imgCount = attrs
            .where((element) =>
                element.docTypeItemAttr?.attrDataType == EAttrDataType.IMGCAPT)
            .length;
        var textCount = attrs
            .where((element) =>
                element.docTypeItemAttr?.attrDataType == EAttrDataType.VARCHAR)
            .length;
        var boolCount = attrs
            .where((element) =>
                element.docTypeItemAttr?.attrDataType == EAttrDataType.BOOLEAN)
            .length;
        txtTotalAttribute.text = "Bạn cần chụp " +
            imgCount.toString() +
            " ảnh, " +
            textCount.toString() +
            " text, " +
            boolCount.toString() +
            " đúng sai";
        setState(() {});
      } catch (ew) {
        toastmessage(ew.toString());
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    var brightness = MediaQuery.of(context).platformBrightness;
    bool isLightMode = brightness == Brightness.light;
    return Container(
      color: isLightMode ? AppTheme.nearlyWhite : AppTheme.nearlyBlack,
      child: SafeArea(
        top: false,
        child: Scaffold(
          backgroundColor:
              isLightMode ? AppTheme.nearlyWhite : AppTheme.nearlyBlack,
          body: SingleChildScrollView(
            child: SizedBox(
              height: MediaQuery.of(context).size.height,
              child: Column(children: <Widget>[
                Padding(
                    padding: EdgeInsets.only(
                        top: MediaQuery.of(context).padding.top,
                        left: 8,
                        right: 8),
                    child: Container(
                      decoration: BoxDecoration(
                        color: HotelAppTheme.buildLightTheme().backgroundColor,
                        boxShadow: <BoxShadow>[
                          BoxShadow(
                              color: Colors.grey.withOpacity(0.2),
                              offset: const Offset(0, 2),
                              blurRadius: 8.0),
                        ],
                      ),
                      child: Padding(
                        padding: EdgeInsets.only(
                            top: MediaQuery.of(context).padding.top,
                            left: 8,
                            right: 8),
                        child: Row(
                          children: <Widget>[
                            Container(
                              alignment: Alignment.centerLeft,
                              width: AppBar().preferredSize.height + 40,
                              height: AppBar().preferredSize.height,
                              child: Material(
                                color: Colors.transparent,
                                child: InkWell(
                                  borderRadius: const BorderRadius.all(
                                    Radius.circular(32.0),
                                  ),
                                  onTap: () {
                                    Navigator.pop(context);
                                  },
                                  child: Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: Icon(Icons.arrow_back),
                                  ),
                                ),
                              ),
                            ),
                            Expanded(
                              child: Center(
                                child: Text(
                                  'Scan VinCode',
                                  style: TextStyle(
                                    fontWeight: FontWeight.w600,
                                    fontSize: 22,
                                  ),
                                ),
                              ),
                            ),
                            const SizedBox(
                              width: 4,
                            ),
                            Container(
                              alignment: Alignment.centerRight,
                              width: AppBar().preferredSize.height + 40,
                              height: AppBar().preferredSize.height,
                              child: Material(
                                color: Colors.transparent,
                                child: InkWell(
                                  borderRadius: const BorderRadius.all(
                                    Radius.circular(32.0),
                                  ),
                                  onTap: () async {
                                    await SessionManager().destroy();
                                    Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                          builder: (context) => loginscreen()),
                                    );
                                  },
                                  child: Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: Icon(Icons.logout),
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    )),
                Row(
                  children: [Text("VinCode")],
                ),
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      Expanded(
                        flex: 3,
                        child: _buildComposer(),
                      ),
                      Expanded(
                        flex: 1,
                        child: ElevatedButton(
                          onPressed: () async {
                            var VinCode = await scanBarcodeNormal();
                            txtVinCode.text = VinCode;
                          },
                          child: const Text('Open Scan'),
                        ),
                      )
                    ]),
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      Expanded(
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: <Widget>[
                            Text(
                              txtModel.text,
                              textAlign: TextAlign.left,
                              style: TextStyle(
                                  fontWeight: FontWeight.w600,
                                  fontSize: 22,
                                  color: Colors.red.withOpacity(0.8)),
                            ),
                            Text(
                              txtTotalAttribute.text,
                              style: TextStyle(
                                  fontSize: 18,
                                  color: Colors.green.withOpacity(0.8)),
                            ),
                            const SizedBox(
                              width: 4,
                            ),
                            /*Row(
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  mainAxisAlignment: MainAxisAlignment.start,
                                  children: <Widget>[
                                    Text(
                                      "modelData!.errorValidate",
                                      style: TextStyle(
                                          fontSize: 14,
                                          color: Colors.red.withOpacity(0.8)),
                                    ),
                                    const SizedBox(
                                      width: 4,
                                    )
                                  ],
                                ),*/
                          ],
                        ),
                      )
                    ]),
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      Container(
                          //width: MediaQuery.of(context).size.width * 0.3,
                          child: Expanded(
                        child: ElevatedButton(
                          onPressed: () async {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => BarCodeScanScreen()),
                            );
                          },
                          child: const Text('Back'),
                        ),
                      )),
                      Container(
                          //width: MediaQuery.of(context).size.width * 0.3,
                          child: Expanded(
                              child: ElevatedButton(
                        onPressed: () async {
                          var sessionManager = SessionManager();
                          await sessionManager.set(
                              "currentVinCode", txtVinCode.text);
                          var currentBarCode = "";
                          sessionManager
                              .get("ViewVinCode")
                              .then((value) => ViewVinCode = value);

                          await sessionManager.get("currentBarcode").then(
                              (value) =>
                                  currentBarCode = value == null ? "" : value);
                          if (ViewVincode != 1 &&
                              currentBarCode != null &&
                              currentBarCode.length > 0) {
                            var getListAttribute =
                                await GetListAttribute(currentBarCode);
                            List<DocTypeItemAddModel> attrs = [];
                            var tagObjsJson =
                                jsonDecode(getListAttribute.body) as List;
                            if (tagObjsJson.length == 0) {
                              toastmessage(
                                  "Không có thuộc tính được cấu hình cho barcode này.");
                              return;
                            }
                          }
                          String temp = currentBarCode + "-" + txtVinCode.text;
                          await sessionManager.set("currentSession", temp);
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => DetailModelScreen()),
                          );
                        },
                        child: const Text('Next'),
                      )))
                    ]),
              ]),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildComposer() {
    return Padding(
      padding: const EdgeInsets.only(top: 16, left: 5, right: 5),
      child: Container(
        decoration: BoxDecoration(
          color: AppTheme.white,
          borderRadius: BorderRadius.circular(8),
          boxShadow: <BoxShadow>[
            BoxShadow(
                color: Colors.grey.withOpacity(0.8),
                offset: const Offset(4, 4),
                blurRadius: 8),
          ],
        ),
        child: ClipRRect(
          borderRadius: BorderRadius.circular(25),
          child: Container(
            padding: const EdgeInsets.all(4.0),
            constraints: const BoxConstraints(minHeight: 80, maxHeight: 160),
            color: AppTheme.white,
            child: SingleChildScrollView(
              padding:
                  const EdgeInsets.only(left: 10, right: 10, top: 0, bottom: 0),
              child: TextField(
                controller: txtVinCode,
                maxLines: null,
                onChanged: (String txt) {},
                style: TextStyle(
                  fontFamily: AppTheme.fontName,
                  fontSize: 16,
                  color: AppTheme.dark_grey,
                ),
                cursorColor: Colors.blue,
                decoration: InputDecoration(
                    border: InputBorder.none, hintText: 'Quét VIN code...'),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
