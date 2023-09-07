import 'dart:convert';
import 'dart:io';
import 'package:VinMediaCapture/apilib/apilib.dart';
import 'package:VinMediaCapture/login/configscreen.dart';
import 'package:VinMediaCapture/objectmodel/configmodel.dart';
import 'package:VinMediaCapture/piaggio/barcode_scan_screen.dart';
import 'package:VinMediaCapture/piaggio/vincode_scan_screen..dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';
import 'package:global_configuration/global_configuration.dart';
import 'package:path_provider/path_provider.dart';
import 'Component.dart';
import 'package:flutter/material.dart';
import 'Toast.dart';
import 'package:flutter/services.dart' show rootBundle;

class MenuScreen extends StatefulWidget {
  const MenuScreen({super.key});

  @override
  State<MenuScreen> createState() => _MenuScreenState();
}

class _MenuScreenState extends State<MenuScreen> {
  bool loading = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          Padding(
            padding: const EdgeInsets.all(0.0),
            child: Column(
              // crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  height: 15,
                ),
                Center(),
                SizedBox(
                  height: 20,
                ),
                Padding(
                  padding: const EdgeInsets.all(2),
                  child: Column(
                    children: [
                      SizedBox(
                        height: 15,
                      ),
                      Text(
                        "Chọn một chức năng",
                        style: TextStyle(
                            color: Colors.black,
                            fontWeight: FontWeight.bold,
                            fontSize: 24),
                      ),
                      SizedBox(
                        height: 45,
                      ),
                      Column(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          IconButton(
                            icon: Image.asset('assets/images/icon-camera.png'),
                            iconSize: 100,
                            onPressed: () async {
                              var sessionManager = SessionManager();
                              await sessionManager.set("ViewVinCode", 0);
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => BarCodeScanScreen()),
                              );
                            },
                          ),
                          const Text(
                            'Chụp Ảnh',
                            style: TextStyle(
                                fontSize: 24, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 10)
                        ],
                      ),
                      Column(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          IconButton(
                            icon: Image.asset(
                                'assets/images/icons8-settings-64.png'),
                            iconSize: 100,
                            onPressed: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => configscreen()),
                              );
                            },
                          ),
                          const Text(
                            'Cấu Hình',
                            style: TextStyle(
                                fontSize: 24, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 10)
                        ],
                      ),
                      Column(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          IconButton(
                            icon: Image.asset('assets/images/motorcycle.png'),
                            iconSize: 100,
                            onPressed: () async {
                              var sessionManager = SessionManager();
                              await sessionManager.set("ViewVinCode", 1);
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                    builder: (context) => VinCodeScanScreen()),
                              );
                            },
                          ),
                          const Text(
                            'Xem lại dữ liệu',
                            style: TextStyle(
                                fontSize: 24, fontWeight: FontWeight.bold),
                          ),
                          const SizedBox(height: 10)
                        ],
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
