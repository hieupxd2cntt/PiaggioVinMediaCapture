import 'package:best_flutter_ui_templates/app_theme.dart';
import 'package:best_flutter_ui_templates/barcode/scanbarcode.dart';
import 'package:best_flutter_ui_templates/hotel_booking/hotel_app_theme.dart';
import 'package:best_flutter_ui_templates/login/Toast.dart';
import 'package:best_flutter_ui_templates/piaggio/detail_model_screen.dart';
import 'package:flutter/material.dart';

class BarCodeScanScreen extends StatefulWidget {
  @override
  _BarCodeScanScreenState createState() => _BarCodeScanScreenState();
}

class _BarCodeScanScreenState extends State<BarCodeScanScreen> {
  TextEditingController txtBarcode = new TextEditingController();
  @override
  void initState() {
    super.initState();
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
                                  'Scan barcode',
                                  style: TextStyle(
                                    fontWeight: FontWeight.w600,
                                    fontSize: 22,
                                  ),
                                ),
                              ),
                            ),
                            Container(
                              width: AppBar().preferredSize.height + 40,
                              height: AppBar().preferredSize.height,
                            )
                          ],
                        ),
                      ),
                    )),
                Row(
                  children: [Text("Barcode")],
                ),
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      Container(
                          //width: MediaQuery.of(context).size.width * 0.7,
                          child: Expanded(
                        child: _buildComposer(),
                      )),
                      Container(
                        //width: MediaQuery.of(context).size.width * 0.3,
                        child: Expanded(
                            child: ElevatedButton(
                          onPressed: () async {
                            var barcode = await scanBarcodeNormal();
                            txtBarcode.text = barcode;
                          },
                          child: const Text('Open Scan'),
                        )),
                      )
                    ]),
                Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                      Container(
                          //width: MediaQuery.of(context).size.width * 0.3,
                          child: Expanded(
                        child: ElevatedButton(
                          onPressed: () async {},
                          child: const Text('Back'),
                        ),
                      )),
                      Container(
                          //width: MediaQuery.of(context).size.width * 0.3,
                          child: Expanded(
                              child: ElevatedButton(
                        onPressed: () async {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => DetailModelScreen(),
                            ),
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
                controller: txtBarcode,
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